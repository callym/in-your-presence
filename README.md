in-your-presence
=====
_a program/performance_

in-your-presence uses non-verbal/visible communication to explore the feeling of sharing a physical space in silence

[view documentation video](https://vimeo.com/163750264)

to run
-----
1. build with visual studio 2015
2. copy the in-your-presence.exe and SharpOSC.dll to a new folder
3. make another copy of in-your-presence.exe
4. (rename the two files, eg. "in-your-presence.exe" -> "me.exe", "in-your-presence - Copy.exe" -> "you.exe")
5. run the two .exe files

technical
-----
communication between the two .exes is through OSC (thanks to the [SharpOSC](https://github.com/ValdemarOrn/SharpOSC) library)

the .exes try to communicate on UDP ports 55555 & 55556, so won't work if them two ports are being used.

they check for the presence of each other every 3 seconds (so you have some time to open the other .exe before the first one closes)
