echo off

rem a1
set a1=..\PIDL\Common\*
rem a2
set a2="C:\Users\shlif\OneDrive\Documents\GitHub\test-detective-client-commit\Assets\Network\Common\*"
xcopy %a1% %a2% /Y

rem b1
set b1=..\PIDL\ServerStucture\*
rem b2
set b2="C:\Users\shlif\OneDrive\Documents\GitHub\test-detective-client-commit\Assets\Network\ServerStucture\*"
xcopy %b1% %b2% /Y

timeout 2