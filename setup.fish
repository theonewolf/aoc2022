#!/usr/bin/env fish

dotnet new sln

for i in (seq 1 25)
    dotnet new console --framework net7.0 -o $i
    echo "foreach (string line in System.IO.File.ReadLines(@\"./sample_input\")) {
    Console.WriteLine(line);
}" > $i/Program.cs
    touch $i/sample_input
    touch $i/input
    dotnet sln add $i/$i.csproj
end
