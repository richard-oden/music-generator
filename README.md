# Music Generator 5000
A neat little console application that procedurally generates a short piece of music with varying degrees of control, ranging from completely at random to completely manual. Pieces are displayed using quasi-sheet music can saved and loaded as JSON files for sharing with your friends. 

## How do I use it?
After compiling, make sure the `Pieces` folder is in the same directory as `music-generator.exe`. Otherwise, the program will generate a new folder. 

Everything should be fairly self-explanatory while running the program, except maybe when writing and viewing pieces. That might be a little confusing, so here is a quick little guide to what those symbols mean. (This can also be found by pressing 'L' while manually writing a new piece.)
```
- S = 1/16th note             - s = 1/16th note rest
- E = 1/8th note              - e = 1/8th note rest
- E. = dotted 1/8th note      - e. = dotted 1/8th note rest
- Q = quarter note            - q = quarter note rest
- Q. = dotted quarter note    - q. = dotted quarter note rest
- H = half note               - h = half note rest
- H. = dotted half note       - h. = dotted half note rest
- W = whole note              - w = whole note rest
- W. = dotted whole note      - w. = dotted whole note rest
```
As a rule, notes and rests are represented as the first letter of their name. Notes are always uppercase and their position on the staff indicates their pitch, while rests are always lowercase and found on the third line of the staff.

## Limitations
As you might have noticed, the program does not support dotted 1/16th notes and rhythms that are shorter than a 1/16th note, as well as rhythms longer than a dotted whole note. A few other notes: 
- Only pitches from C4 to B5 are possible.
- Key Signatures cannot contain double sharps or double flats.
- No irregular time signatures are permitted at the moment.
