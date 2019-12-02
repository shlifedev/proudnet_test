echo off

rem a1
set a1=..\GameServer\bin\Debug\TableDatas\*
rem a2
set a2="C:\Users\shlif\New Unity Project\Assets\Resources\TableDatas\*"
xcopy %a1% %a2% /Y
 
echo -Start Table CS Copy- 

rem b1
set b1=..\PIDL\Table\*
rem b2
set b2="C:\Users\shlif\New Unity Project\Assets\Scripts\Table\Cs\*"
xcopy %b1% %b2% /Y