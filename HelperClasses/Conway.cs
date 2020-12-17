using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class AllCubes
    {
        Dictionary<ConwayCube, List<ConwayCube>> CubesAndNeighbours
            = new Dictionary<ConwayCube, List<ConwayCube>>();
        public int Cycle { get; private set; }
        public int ActiveCubes { get => GetActiveCount(); }
        public AllCubes(List<string> startState)
        {
            Cycle = 1;
            int startSizeY = startState.Count;
            int startSizeX = startState[0].Length;
            bool state;

            // Creates the initial Cubes
            for (int y = 0; y < startSizeY; y++)
            {
                for (int x = 0; x < startSizeX; x++)
                {
                    state = startState[y][x] == '#';
                    CubesAndNeighbours[new ConwayCube(x, y, 0, state)] = new List<ConwayCube>();
                }
            }
            // Gives each cubes its list of neighbours
            UpdateNeighbours(CubesAndNeighbours.Keys.ToList());
        }

        public void NewCycle()
        {
            Cycle++;
            CreateNewCubes();
            List<ConwayCube> allCubes = CubesAndNeighbours.Keys.ToList();
            // Sets whether or not a cube is switching state this cycle
            foreach (ConwayCube cube in allCubes)
            {
                cube.CheckForSwitch(CubesAndNeighbours[cube]);
            }

            // Switches states on the cubes (if applicable)
            foreach (ConwayCube cube in allCubes)
            {
                cube.SwitchState();

                if (cube.State && cube.ActiveNeighbours == 0)
                    CleanUpCube(cube);
            }
        }

        // Finds active cubes with less than 26 neighbours and creates the missing neighbours
        private void CreateNewCubes()
        {
            List<ConwayCube> allCubes = CubesAndNeighbours.Keys.ToList();
            foreach (ConwayCube cube in allCubes)
            {
                // 26 is max number of neighbours a hypercube can have (3^3-1)
                if (cube.State && CubesAndNeighbours[cube].Count < 26)
                {
                    CreateNeighbours(cube);
                }
            }
        }

        // Creates missing neighbours for a given cube
        private void CreateNeighbours(ConwayCube cube)
        {
            int startX = cube.Coordinates.x;
            int startY = cube.Coordinates.y;
            int startZ = cube.Coordinates.z;
            ConwayCube tempCube;
            List<ConwayCube> newCubes = new List<ConwayCube>();

            for (int x = startX - 1; x <= startX + 1; x++)
            {
                for (int y = startY - 1; y <= startY + 1; y++)
                {
                    for (int z = startZ - 1; z <= startZ + 1; z++)
                    {
                        // if the current coordinates is not the cube or does not already exists
                        if (!(cube.Equals(x, y, z) || CubesAndNeighbours[cube].Exists(check => check.Equals(x, y, z))))
                        {
                            tempCube = new ConwayCube(x, y, z);

                            newCubes.Add(tempCube);
                            // the new cubes dicts is initialised with the current cube
                            CubesAndNeighbours[tempCube] = new List<ConwayCube>
                                {
                                    cube,
                                };
                            // new cube is added to the cubes neighbours
                            CubesAndNeighbours[cube].Add(tempCube);
                        }
                    }
                }
            }
            UpdateNeighbours(newCubes);
        }

        // Given a list of cubes, updates the dictionary with the new neighbours
        private void UpdateNeighbours(List<ConwayCube> newCubes)
        {
            List<ConwayCube> allCubes = CubesAndNeighbours.Keys.ToList();
            // Inits Neighbours
            foreach (ConwayCube checkCube in newCubes)
            {
                foreach (ConwayCube potNeighbour in allCubes)
                {
                    // If a cube has not already been added to the neighbourList
                    if (!CubesAndNeighbours[checkCube].Contains(potNeighbour) &&
                        checkCube.IsNeighbour(potNeighbour)) // Check if the cube is a neighbour
                    {
                        // Add the two cubes to each others list
                        CubesAndNeighbours[checkCube].Add(potNeighbour);
                        CubesAndNeighbours[potNeighbour].Add(checkCube);
                    }
                }
            }
        }

        private void CleanUpCube(ConwayCube cube)
        {
            foreach (ConwayCube neighbour in CubesAndNeighbours[cube])
            {
                CubesAndNeighbours[neighbour].Remove(cube);
            }
            CubesAndNeighbours.Remove(cube);
        }

        private int GetActiveCount()
        {
            int counter = 0;
            foreach (ConwayCube cube in CubesAndNeighbours.Keys)
            {
                if (cube.State)
                    counter++;
            }
            return counter;
        }
    }

    public class ConwayCube : IEquatable<ConwayCube>, IEquatable<XYZ>
    {
        public XYZ Coordinates { get; }
        public bool State { get; private set; }
        public bool ToBeSwitched { get; private set; }

        public int ActiveNeighbours { get; private set; }

        public ConwayCube(XYZ coordinates, bool state = false)
        {
            Coordinates = coordinates;
            this.State = state;
            ToBeSwitched = false;
        }

        public ConwayCube(int x, int y, int z, bool state = false)
        {
            Coordinates = new XYZ(x, y, z);
            this.State = state;
            ToBeSwitched = false;
        }

        // Determines if a cube should be switched next Cycle
        public void CheckForSwitch(List<ConwayCube> neighbours)
        {
            ActiveNeighbours = 0;
            foreach (ConwayCube neighbour in neighbours)
            {
                if (neighbour.State)
                    ActiveNeighbours++;
                if (ActiveNeighbours > 3)
                {
                    ToBeSwitched = State ? true : false;
                    return;
                }
            }
            // if the state is active(true) and it does not have 2 or 3 active neighbours it switches
            ToBeSwitched = ((State && !(ActiveNeighbours == 2 || ActiveNeighbours == 3)) ||
                // if the state is inactive and has exactly three neighbours it switches
                (!State && ActiveNeighbours == 3));         
        }

        public void SwitchState()
        {
            if (ToBeSwitched)
            {
                State = !State;
                ToBeSwitched = false;
            }
        }

        public bool IsNeighbour(ConwayCube potNeighbour)
        {
            return Coordinates.IsNeighbour(potNeighbour.Coordinates);
        }

        public bool Equals(ConwayCube other)
        {
            return Coordinates.Equals(other.Coordinates);
        }

        public bool Equals(XYZ coordinates)
        {
            return Coordinates.Equals(coordinates);
        }

        public bool Equals(int x, int y, int z)
        {
            return Coordinates.Equals(x, y, z);
        }
    }

    // coordinate struct for 3 spacial coordinates
    public struct XYZ : IEquatable<XYZ>
    {
        public int x { get; }
        public int y { get; }
        public int z { get; }

        public XYZ(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public bool Equals(XYZ other)
        {
            return x == other.x &&
                y == other.y &&
                z == other.z;
        }

        public bool Equals(int x, int y, int z)
        {
            return this.x == x &&
                this.y == y &&
                this.z == z;
        }

        public bool IsNeighbour(XYZ other)
        {
            // coordinate is a neighbour if it's not the same coordinate
            // and is within +- 1 of all the other coordinates
            return (!(Equals(other)) &&
                (x >= other.x - 1 && x <= other.x + 1) &&
                (y >= other.y - 1 && y <= other.y + 1) &&
                (z >= other.z - 1 && z <= other.z + 1));
        }
    }

    public class AllHyperCubes
    {
        Dictionary<ConwayHyperCube, List<ConwayHyperCube>> CubesAndNeighbours
            = new Dictionary<ConwayHyperCube, List<ConwayHyperCube>>();
        public int Cycle { get; private set; }
        public int ActiveCubes { get => GetActiveCount(); }
        public AllHyperCubes(List<string> startState)
        {
            Cycle = 1;

            int startSizeY = startState.Count;
            int startSizeX = startState[0].Length;
            bool state;

            // Creates the initial Cubes
            for (int y = 0; y < startSizeY; y++)
            {
                for (int x = 0; x < startSizeX; x++)
                {
                    state = startState[y][x] == '#';
                    CubesAndNeighbours[new ConwayHyperCube(x, y, 0, 0, state)] = new List<ConwayHyperCube>();
                }
            }
            // Gives each cubes its list of neighbours
            UpdateNeighbours(CubesAndNeighbours.Keys.ToList());
            // creates new cubes around active cubes and updates the dictionary lists
        }

        public void NewCycle()
        {
            Cycle++;
            CreateNewCubes();
            // Sets whether or not a cube is switching state this cycle
            foreach (ConwayHyperCube cube in CubesAndNeighbours.Keys)
            {
                cube.CheckForSwitch(CubesAndNeighbours[cube]);
            }

            // Switches states on the cubes (if applicable)
            foreach (ConwayHyperCube cube in CubesAndNeighbours.Keys)
            {
                cube.SwitchState();
                //if (!cube.State && cube.ActiveNeighbours == 0)
                //    CleanUpCube(cube);
            }
        }

        // Finds active cubes with less than 80 neighbours and creates the missing neighbours
        private void CreateNewCubes()
        {
            List<ConwayHyperCube> AllHyperCubes = CubesAndNeighbours.Keys.ToList();
            foreach (ConwayHyperCube cube in AllHyperCubes)
            {
                // 80 is max number of neighbours a hypercube can have 3^4-1
                if (cube.State && CubesAndNeighbours[cube].Count < 80)
                {
                    CreateNeighbours(cube);
                }
            }
        }

        // Creates missing neighbours for a given cube
        private void CreateNeighbours(ConwayHyperCube cube)
        {
            int startX = cube.Coordinates.x;
            int startY = cube.Coordinates.y;
            int startZ = cube.Coordinates.z;
            int startW = cube.Coordinates.w;
            ConwayHyperCube tempCube;
            List<ConwayHyperCube> newCubes = new List<ConwayHyperCube>();

            for (int x = startX - 1; x <= startX + 1; x++)
            {
                for (int y = startY - 1; y <= startY + 1; y++)
                {
                    for (int z = startZ - 1; z <= startZ + 1; z++)
                    {
                        for (int w = startW - 1; w <= startW + 1; w++)
                        {
                            // if the current coordinates is not the cube or does not already exists
                            if (!(cube.Equals(x, y, z, w) || CubesAndNeighbours[cube].Exists(check => check.Equals(x, y, z, w))))
                            {
                                tempCube = new ConwayHyperCube(x, y, z, w);

                                newCubes.Add(tempCube);
                                // the new cubes dicts is initialised with the current cube
                                CubesAndNeighbours[tempCube] = new List<ConwayHyperCube>
                                {
                                    cube,
                                };
                                // new cube is added to the cubes neighbours
                                CubesAndNeighbours[cube].Add(tempCube);
                            }
                        }
                    }
                }
            }
            UpdateNeighbours(newCubes);
        }

        // Given a list of cubes, updates the dictionary with the new neighbours
        private void UpdateNeighbours(List<ConwayHyperCube> newCubes)
        {
            List<ConwayHyperCube> AllHyperCubes = CubesAndNeighbours.Keys.ToList();
            // Inits Neighbours
            foreach (ConwayHyperCube checkCube in newCubes)
            {
                foreach (ConwayHyperCube potNeighbour in AllHyperCubes)
                {
                    // If a cube has not already been added to the neighbourList
                    if (!CubesAndNeighbours[checkCube].Contains(potNeighbour) &&
                        checkCube.IsNeighbour(potNeighbour)) // Check if the cube is a neighbour
                    {
                        // Add the two cubes to each others list
                        CubesAndNeighbours[checkCube].Add(potNeighbour);
                        CubesAndNeighbours[potNeighbour].Add(checkCube);
                    }
                }
            }
        }

        private void CleanUpCube(ConwayHyperCube cube)
        {
            foreach (ConwayHyperCube neighbour in CubesAndNeighbours[cube])
            {
                CubesAndNeighbours[neighbour].Remove(cube);
            }
            CubesAndNeighbours.Remove(cube);
        }

        private int GetActiveCount()
        {
            int counter = 0;
            foreach (ConwayHyperCube cube in CubesAndNeighbours.Keys)
            {
                if (cube.State)
                    counter++;
            }
            return counter;
        }
    }

    public class ConwayHyperCube : IEquatable<ConwayHyperCube>, IEquatable<XYZW>
    {
        public XYZW Coordinates { get; }
        public bool State { get; private set; }
        public bool ToBeSwitched { get; private set; }

        public int ActiveNeighbours { get; private set; }


        public ConwayHyperCube(int x, int y, int z, int w, bool state = false)
        {
            Coordinates = new XYZW(x, y, z, w);
            this.State = state;
            ToBeSwitched = false;
        }

        // Determines if a cube should be switched next Cycle
        public void CheckForSwitch(List<ConwayHyperCube> neighbours)
        {
            ActiveNeighbours = 0;
            foreach (ConwayHyperCube neighbour in neighbours)
            {
                if (neighbour.State)
                    ActiveNeighbours++;
                if (ActiveNeighbours > 3)
                {
                    ToBeSwitched = State ? true : false;
                    return;
                }
            }
            // if the state is active(true) and it does not have 2 or 3 active neighbours it switches
            ToBeSwitched = ((State && !(ActiveNeighbours == 2 || ActiveNeighbours == 3)) ||
                // if the state is inactive and has exactly three neighbours it switches
                (!State && ActiveNeighbours == 3));
        }

        public void SwitchState()
        {
            if (ToBeSwitched)
            {
                State = !State;
                ToBeSwitched = false;
            }
        }

        public bool IsNeighbour(ConwayHyperCube potNeighbour)
        {
            return Coordinates.IsNeighbour(potNeighbour.Coordinates);
        }

        public bool Equals(ConwayHyperCube other)
        {
            return Coordinates.Equals(other.Coordinates);
        }

        public bool Equals(XYZW coordinates)
        {
            return Coordinates.Equals(coordinates);
        }

        public bool Equals(int x, int y, int z, int w)
        {
            return Coordinates.Equals(x, y, z, w);
        }
    }




    // coordinate struct for 4 spacial coordinates
    public struct XYZW : IEquatable<XYZW>
    {
        public int x { get; }
        public int y { get; }
        public int z { get; }
        public int w { get; }

        public XYZW(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public bool Equals(XYZW other)
        {
            return x == other.x &&
                y == other.y &&
                z == other.z &&
                w == other.w;

        }

        public bool Equals(int x, int y, int z, int w)
        {
            return this.x == x &&
                this.y == y &&
                this.z == z &&
                this.w == w;
        }

        public bool IsNeighbour(XYZW other)
        {
            // coordinate is a neighbour if it's not the same coordinate
            // and is within +- 1 of all the other coordinates
            return (!(Equals(other)) &&
                (x >= other.x - 1 && x <= other.x + 1) &&
                (y >= other.y - 1 && y <= other.y + 1) &&
                (z >= other.z - 1 && z <= other.z + 1) &&
                (w >= other.w - 1 && w <= other.w + 1));
        }


    }
}

