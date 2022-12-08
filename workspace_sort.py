#!/usr/bin/env python3
from json import dump, load

workspace = load(open('AdventOfCode2022.code-workspace'))

# First component is string '.' and can't be parsed as an int.
paths = workspace['folders'][1:]
paths.sort(key=lambda x: int(x['path']))
workspace['folders'] = workspace['folders'][0:1] + paths

dump(workspace, open('AdventOfCode2022.code-workspace','w'))
