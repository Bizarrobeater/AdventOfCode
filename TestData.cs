using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class TestData
    {
        public static List<string> Dec7Part1 = new List<string>
        {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags.",
        };

        public static List<string> Dec7Part2 = new List<string>
        {
                "shiny gold bags contain 2 dark red bags.",
                "dark red bags contain 2 dark orange bags.",
                "dark orange bags contain 2 dark yellow bags.",
                "dark yellow bags contain 2 dark green bags.",
                "dark green bags contain 2 dark blue bags.",
                "dark blue bags contain 2 dark violet bags.",
                "dark violet bags contain no other bags.",
        };

        public static List<string> Dec8 = new List<string>
        {
            "nop +0",
            "acc +1",
            "jmp +4",
            "acc +3",
            "jmp -3",
            "acc -99",
            "acc +1",
            "jmp -4",
            "acc +6",
        };

        public static List<long> Dec9 = new List<long>
        {
            35,
            20,
            15,
            25,
            47,
            40,
            62,
            55,
            65,
            95,
            102,
            117,
            150,
            182,
            127,
            219,
            299,
            277,
            309,
            576,
        };


        // Solution for Part1 should be 220
        // Solution for Part2 should be 19208
        public static List<int> Dec10 = new List<int>
        {
            28,
            33,
            18,
            42,
            31,
            14,
            46,
            20,
            48,
            47,
            24,
            23,
            49,
            45,
            19,
            38,
            39,
            11,
            1,
            32,
            25,
            35,
            8,
            17,
            7,
            9,
            4,
            2,
            34,
            10,
            3,
        };

        public static List<string> Dec11Part1 = new List<string>
        {
            "L.LL.LL.LL",
            "LLLLLLL.LL",
            "L.L.L..L..",
            "LLLL.LL.LL",
            "L.LL.LL.LL",
            "L.LLLLL.LL",
            "..L.L.....",
            "LLLLLLLLLL",
            "L.LLLLLL.L",
            "L.LLLLL.LL",
        };

        public static List<string> Dec12Part1 = new List<string>
        {
            "F10",
            "N3",
            "F7",
            "R90",
            "F11",
        };

        // Solution for part 1: 295
        public static List<string> Dec13Part1 = new List<string>
        {
            "939",
            "7,13,x,x,59,x,31,19",
        };

        // Dictionary of example list and their correct data
        public static Dictionary<string, long> Dec13Part2 = new Dictionary<string, long>
        {
            {"3,x,7", 12 },
            {"7,x,3", 7 },
            {"6,x,x,x,17", 30 },
            {"7,x,x,4,x,11", 105 },
            { "7,13,x,x,59,x,31,19", 1_068_781 },
            { "17,x,13,19", 3_417},
            { "67,7,59,61", 754_018},
            { "67,x,7,59,61", 779_210},
            { "67,7,x,59,61", 1_261_476},
            { "1789,37,47,1889", 1_202_161_486},
        };

        // Solution for part 1: 165
        public static List<string> Dec14Part1 = new List<string>
        {
            "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
            "mem[8] = 11",
            "mem[7] = 101",
            "mem[8] = 0",
        };
    }
}
