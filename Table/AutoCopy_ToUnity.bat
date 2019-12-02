echo off

rem originalPath
set originalPath=xlsx\*
rem copyPath
set copyPath=xlsx2\*


xcopy %originalPath% %copyPath%
