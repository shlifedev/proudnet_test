echo off

::엑셀파일이 존재하는 경로 입력하세요
::ex) c:\....\excels
rem excelPath
set excelPath=xlsx

::cs파일을 저장할 경로
::ex) c:\....\cs
rem csPath
set csPath=C:\Users\shlif\Documents\GitHub\MyDetectiveServer\GameServer\Table

::json파일을 저장할 경로
::ex) c:\....\json
rem jsonPath
set jsonPath=C:\Users\shlif\Documents\GitHub\MyDetectiveServer\GameServer\bin\Debug\TableDatas

TB.exe %excelPath% %csPath% %jsonPath%

pause