namespace Advent_of_Code_2022.Days;

public class Day07 : DayBase<Day07.Directory, int>
{
    public override string Name => "Day 7: No Space Left On Device";
    
    public override Directory ParseInput(IEnumerable<string> lines)
    {
        var root = new Directory("/");
        var currentDirectory = root;

        foreach (var line in lines.Skip(1))
        {
            if (line.StartsWith("$ "))
            {
                switch (line[2..])
                {
                    case "ls":
                        break;
                    case "cd ..":
                        currentDirectory = currentDirectory.ParentDirectory!;
                        break;
                    default:
                        currentDirectory = currentDirectory.SubDirectories.First(d => d.Name == line[5..]);
                        break;
                }
            }
            else if (line.StartsWith("dir "))
            {
                var directory = new Directory(line[4..], currentDirectory);
                currentDirectory.SubDirectories.Add(directory);
            }
            else
            {
                var parts = line.Split(' ');
                var file = new File(parts[1], currentDirectory, int.Parse(parts[0]));
                currentDirectory.Files.Add(file);
            }
        }

        return root;
    }

    public override int RunPart1(Directory input)
        => input.GetAllDirectories().Select(d => d.TotalSize()).Where(s => s <= 100000).Sum();

    public override int RunPart2(Directory input)
    {
        const int fileSystemSize = 70000000;
        const int requiredSpace = 30000000;
        var occupiedSpace = input.TotalSize();
        var unusedSpace = fileSystemSize - occupiedSpace;
        var spaceToFreeUp = requiredSpace - unusedSpace;

        return input
            .GetAllDirectories()
            .OrderBy(d => d.TotalSize())
            .First(d => d.TotalSize() >= spaceToFreeUp)
            .TotalSize();
    }
    
    public class Directory
    {
        public string Name { get; }
        public Directory? ParentDirectory { get; }
        public List<Directory> SubDirectories { get; } = new();
        public List<File> Files { get; } = new();

        public Directory(string name, Directory? parentDirectory = default)
        {
            Name = name;
            ParentDirectory = parentDirectory;
        }

        public int FileSize()
            => Files.Sum(f => f.Size);
        
        public int TotalSize()
            => SubDirectories.Sum(s => s.TotalSize()) + FileSize();

        public IEnumerable<Directory> GetAllDirectories()
        {
            var result = new List<Directory> { this };
            result.AddRange(SubDirectories.SelectMany(s => s.GetAllDirectories()));
            return result;
        }
    }

    public class File
    {
        public string Name { get; }
        public Directory ParentDirectory { get; }
        public int Size { get; }

        public File(string name, Directory parentDirectory, int size)
        {
            Name = name;
            ParentDirectory = parentDirectory;
            Size = size;
        }
    }
}
