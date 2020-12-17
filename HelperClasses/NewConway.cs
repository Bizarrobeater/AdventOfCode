using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.HelperClassses
{
    public class ConwayCubeManager
    {
        ConwayCubeComparer cubeComp = new ConwayCubeComparer();
        HashSet<ConwayCube> conwayCubes;
        int maxNeighbours = 26;
        public int Cycle { get; private set; }
        public int ActiveCubes { get => GetActiveCount(); }
        public ConwayCubeManager(List<string> startState)
        {
            conwayCubes = new HashSet<ConwayCube>(cubeComp);
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
                    conwayCubes.Add(new ConwayCube(x, y, 0, state));
                }
            }
            // Gives each cubes its list of neighbours
            UpdateNeighbours(conwayCubes);

            foreach (ConwayCube cube in conwayCubes)
            {
                cube.CountActiveNeighbours();
            }
        }

        public void NewCycle()
        {
            Cycle++;
            CreateNewCubes();
            // Sets whether or not a cube is switching state this cycle
            foreach (ConwayCube cube in conwayCubes)
            {
                cube.CheckForSwitch();
            }
        }

        private void UpdateNeighbours(HashSet<ConwayCube> newCubes)
        {
            // Inits Neighbours
            foreach (ConwayCube checkCube in newCubes)
            {
                foreach (ConwayCube potNeighbour in conwayCubes)
                {
                    // If a cube has not already been added to the neighbourList
                    if (!checkCube.Neighbours.Contains(potNeighbour) &&
                        checkCube.IsNeighbour(potNeighbour)) // Check if the cube is a neighbour
                    {
                        // Add the two cubes to each others list
                        checkCube.AddNeighbour(potNeighbour);
                        potNeighbour.AddNeighbour(checkCube);
                    }
                }
                checkCube.CountActiveNeighbours();
            }
            conwayCubes.UnionWith(newCubes);
        }

        private void CreateNewCubes()
        {
            HashSet<ConwayCube> getNeighbours = new HashSet<ConwayCube>();
            foreach (ConwayCube cube in conwayCubes)
            {
                cube.SnapShot();
                if (cube.State && cube.amountNeighbours < maxNeighbours)
                    getNeighbours.Add(cube);
            }
            CreateNeighbours(getNeighbours);
        }

        private void CreateNeighbours(HashSet<ConwayCube> cubes)
        {
            int startX; 
            int startY;
            int startZ;

            ConwayCube newCube;
            HashSet<ConwayCube> newCubes = new HashSet<ConwayCube>();
            foreach (ConwayCube cube in cubes)
            {
                startX = cube.Coordinates.x;
                startY = cube.Coordinates.y;
                startZ = cube.Coordinates.z;

                for (int x = startX - 1; x <= startX + 1; x++)
                {
                    for (int y = startY - 1; y <= startY + 1; y++)
                    {
                        for (int z = startZ - 1; z <= startZ + 1; z++)
                        {
                            newCube = new ConwayCube(x, y, z);
                            // if the new cube doesn't already exist add it
                            if (!conwayCubes.Contains(newCube))
                            {
                                newCubes.Add(newCube);
                            }
                        }
                    }
                }
            }
            // Update the entire set with the new neighbours
            UpdateNeighbours(newCubes);
        }

        private int GetActiveCount()
        {
            int counter = 0;
            foreach (ConwayCube cube in conwayCubes.Where(a => a.State))
            {
                counter++;
            }
            return counter;
        }
    }

    // Class For Conways Game Of Life in 3 dimensions
    public class ConwayCube : IEquatable<ConwayCube>, IEquatable<XYZ>
    {
        public XYZ Coordinates { get; }
        public HashSet<ConwayCube> Neighbours { get; private set; }
        public int amountNeighbours { get; private set; }
        public bool State { get; private set; }

        public int ActiveNeighbours { get; set; }
        public int SnapShotNeighbours { get; private set; }

        public ConwayCube(XYZ coordinates, bool state = false)
        {
            Coordinates = coordinates;
            this.State = state;

            Neighbours = new HashSet<ConwayCube>();
        }

        public void SnapShot()
        {
            SnapShotNeighbours = ActiveNeighbours;
        }

        public ConwayCube(int x, int y, int z, bool state = false)
        {
            Coordinates = new XYZ(x, y, z);
            this.State = state;
            Neighbours = new HashSet<ConwayCube>();
        }

        // Determines if a cube should be switched next Cycle
        public void CheckForSwitch()
        {
            if ((State && !(SnapShotNeighbours == 2 || SnapShotNeighbours == 3)) ||
                (!State && SnapShotNeighbours == 3))
                SwitchState();
        }

        public void CountActiveNeighbours()
        {
            ActiveNeighbours = 0;
            foreach (ConwayCube neighbour in Neighbours)
            {
                if (neighbour.State)
                    ActiveNeighbours++;
            }
            SnapShot();
        }

        public void AddNeighbour(ConwayCube newNeighbour)
        {
            Neighbours.Add(newNeighbour);
            amountNeighbours++;
        }

        private void SwitchState()
        {
            State = !State;
            foreach (ConwayCube neighbour in Neighbours)
            {
                if (State)
                    neighbour.ActiveNeighbours++;
                else
                    neighbour.ActiveNeighbours--;
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

        public override int GetHashCode()
        {
            return Coordinates.GetHashCode();
        }
    }

    // Struct for Coordinates in a 3D space
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

        public override int GetHashCode()
        {
            return x ^ y ^ z;
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

    class ConwayCubeComparer : IEqualityComparer<ConwayCube>
    {
        public bool Equals(ConwayCube x, ConwayCube y)
        {
            return x.Equals(y);
        }

        public bool Equals(ConwayCube x, XYZ y)
        {
            return x.Equals(y);
        }

        public bool Equals(ConwayCube a, int x, int y, int z)
        {
            return a.Equals(x, y, z);
        }

        public int GetHashCode([DisallowNull] ConwayCube obj)
        {
            return obj.GetHashCode();
        }
    }




}
