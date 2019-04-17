---
title: "Unsupported Visual FoxPro Commands and Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "FoxPro ODBC driver [ODBC], commands and functions"
  - "functions [ODBC], Visual FoxPro"
  - "Visual FoxPro ODBC driver [ODBC], commands and functions"
  - "Visual FoxPro commands and functions"
  - "FoxPro ODBC driver"
ms.assetid: afdb6b7e-738d-42ca-8053-67ae50873ca6
author: MightyPen
ms.author: genemi
manager: craigg
---
# Unsupported Visual FoxPro Commands and Functions (Visual FoxPro ODBC Driver)
The following table lists FoxPro commands and functions that are not supported by the Visual FoxPro ODBC Driver but are supported by Microsoft® Visual FoxPro®.  
  
 If your application interacts with data whose rules, triggers, default values, or stored procedures call these Visual FoxPro commands or functions, the driver can generate an error.  
  
## Unsupported Visual FoxPro Commands and Functions  
  
||||  
|-|-|-|  
|#DEFINE ... #UNDEF|#IF ... #ENDIF Preprocessor Directive|#IFDEF &#124; #IFNDEF|  
|#INCLUDE Preprocessor Directive|:: Scope Resolution Operator|! Command (see RUN &#124; ! Command)|  
|? &#124; ?? Command|??? Command|\ &#124; \\\ Command|  
|@ ... BOX Command|@ ... CLASS Command|@ ... CLEAR Command|  
|@ ... EDIT - Edit Boxes Command|@ ... FILL Command|@ ... GET|  
|@ ... MENU Command|@ ... PROMPT Command|@ ... SAY Command|  
|@ ... SCROLL Command|@ ... TO Command||  
  
## A  
  
||||  
|-|-|-|  
|ACCEPT Command|ACLASS( ) Function|ACTIVATE MENU Command|  
|ACTIVATE POPUP Command|ACTIVATE SCREEN Command|ACTIVATE WINDOW Command|  
|ActivateCell Method|ADD CLASS Command|ADIR( ) Function|  
|AFONT( ) Function|AINSTANCE( ) Function|_ALIGNMENT System Memory Variable|  
|AMEMBERS( ) Function|ANSITOOEM( ) Function|APRINTERS( ) Function|  
|ASELOBJ( ) Function|ASSIST Command||  
  
## B  
  
||||  
|-|-|-|  
|BAR( ) Function|BARCOUNT( ) Function|BARPROMPT( ) Function|  
|_BEAUTIFY System Memory Variable|_BOX System Memory Variable|BROWSE Command|  
|_BROWSER System Memory Variable|BUILD APP Command|BUILD EXE Command|  
|BUILD PROJECT Command|_BUILDER System Memory Variable||  
  
## C  
  
||||  
|-|-|-|  
|_CALCVALUE System Memory Variable|_CLIPTEXT System Memory Variable|_CONVERTER System Memory Variable|  
|_CUROBJ System Memory Variable|CALL Command|CANCEL Command|  
|CAPSLOCK( ) Function|CD Command|CHANGE Command|  
|CHDIR Command|CHRSAW( ) Function|CLOSE MEMO Command|  
|CNTBAR( ) Function|CNTPAD( ) Function|COL( ) Function|  
|COMPILE Command|COMPILE DATABASE Command|COMPILE FORM Command|  
|COMPOBJ( ) Function|Container Object|Control Object|  
|COPY FILE Command|COPY MEMO Command|CREATE CLASS Command|  
|CREATE CLASSLIB Command|CREATE COLOR SET Command|CREATE Command|  
|CREATE CONNECTION Command|CREATE DATABASE Command|CREATE FORM Command|  
|CREATE FROM Command|CREATE LABEL Command|CREATE MENU Command|  
|CREATE PROJECT Command|CREATE QUERY Command|CREATE REPORT Command|  
|CREATE SCREEN Command|CREATE SQL VIEW Command|CREATE TRIGGER Command|  
|CREATE VIEW Command|CREATEOBJECT( ) Function|CURDIR( ) Function|  
  
## D  
  
||||  
|-|-|-|  
|_DBLCLICK System Memory Variable|_DIARYDATE System Memory Variable|DBSETPROP( ) Function|  
|DDE Functions|DEACTIVATE MENU Command|DEACTIVATE POPUP Command|  
|DEACTIVATE WINDOW Command|DECLARE - DLL Command|DECLARE Command|  
|DEFINE BAR Command|DEFINE BOX Command|DEFINE CLASS Command|  
|DEFINE MENU Command|DEFINE PAD Command|DEFINE POPUP Command|  
|DEFINE WINDOW Command|DELETE CONNECTION Command|DELETE DATABASE Command|  
|DELETE FILE Command|DELETE TRIGGER Command|DELETE VIEW Command|  
|DIR Command|DIRECTORY Command|DISPLAY Command|  
|DISPLAY CONNECTIONS Command|DISPLAY DATABASE Command|DISPLAY DLLS Command|  
|DISPLAY FILES Command|DISPLAY MEMORY Command|DISPLAY OBJECTS Command|  
|DISPLAY PROCEDURES Command|DISPLAY STATUS Command|DISPLAY STRUCTURE Command|  
|DISPLAY TABLES Command|DISPLAY VIEWS Command|DO FORM Command|  
  
## E  
  
||||  
|-|-|-|  
|EDIT Command|ERROR Command||  
|ERASE Command|EXTERNAL Command|EXPORT Command|  
|EJECT Command|EJECT PAGE Command||  
  
## F  
  
||||  
|-|-|-|  
|_FOXDOC System Memory Variable|_FOXGRAPH System Memory Variable|FEOF( ) Function|  
|FCLOSE( ) Function|FCREATE( ) Function|FGETS( ) Function|  
|FERROR( ) Function|FFLUSH( ) Function|FKLABEL( ) Function|  
|FILER Command|FIND Command|FOPEN( ) Function|  
|FKMAX( ) Function|FONTMETRIC( ) Function|FSEEK( ) Function|  
|FPUTS( ) Function|FREAD( ) Function||  
|FWRITE( ) Function|FCHSIZE( ) Function||  
  
## G  
  
||||  
|-|-|-|  
|_GENGRAPH System Memory Variable|_GENMENU System Memory Variable|_GENPD System Memory Variable|  
|_GENSCRN System Memory Variable|_GENXTAB System Memory Variable|GETBAR( ) Function|  
|GETCOLOR( ) Function|GETDIR( ) Function|GETEXPR Command|  
|GETFILE( ) Function|GETFONT( ) Function|GETOBJECT( ) Function|  
|GETPAD( ) Function|GETPICT( ) Function|GETPRINTER( ) Function|  
  
## H  
  
||||  
|-|-|-|  
|HELP Command|HIDE MENU Command|HIDE POPUP Command|  
|HIDE WINDOW Command|HOME( ) Function||  
  
## I  
  
||||  
|-|-|-|  
|IMESTATUS( ) Function|IMPORT Command|INPUT Command|  
|INDEX ON Command|INKEY( ) Function|ISCOLOR( ) Function|  
|INSERT Command|INSMODE( ) Function||  
|ISMOUSE( ) Function|_INDENT System Memory Variable||  
  
## J  
  
||||  
|-|-|-|  
|JOIN Command|||  
  
## K  
  
||||  
|-|-|-|  
|KEYBOARD Command|||  
  
## L  
  
||||  
|-|-|-|  
|_LMARGIN System Memory Variable|LABEL Command|LASTKEY( ) Function|  
|LINENO( ) Function|LIST Commands|LIST CONNECTIONS Command|  
|LOAD Command|LOCFILE( ) Function||  
  
## M  
  
||||  
|-|-|-|  
|MCOL( ) Function|MD Command|MENU TO Command|  
|MEMORY( ) Function|MENU Command|MKDIR Command|  
|MENU( ) Function|MESSAGEBOX( ) Function|MODIFY CONNECTION Command|  
|MODIFY CLASS Command|MODIFY COMMAND Command|MODIFY FORM Command|  
|MODIFY DATABASE Command|MODIFY FILE Command|MODIFY MEMO Command|  
|MODIFY GENERAL Command|MODIFY LABEL Command|MODIFY PROJECT Command|  
|MODIFY MENU Command|MODIFY PROCEDURE Command|MODIFY SCREEN Command|  
|MODIFY QUERY Command|MODIFY REPORT Command|MODIFY WINDOW Command|  
|MODIFY STRUCTURE Command|MODIFY VIEW Command|MOVE WINDOW Command|  
|MOUSE Command|MOVE POPUP Command|MROW( ) Function|  
|MRKBAR( ) Function|MRKPAD( ) Function||  
|MWINDOW( ) Function|MDOWN( ) Function||  
  
## N  
  
||||  
|-|-|-|  
|NUMLOCK( ) Function|||  
  
## O  
  
||||  
|-|-|-|  
|OBJNUM( ) Function|OBJTOCLIENT( ) Function|ON BAR Command|  
|OEMTOANSI( ) Function|ON APLABOUT Command|ON EXIT MENU Command|  
|ON ESCAPE Command|ON EXIT BAR Command|ON KEY = Command|  
|ON EXIT PAD Command|ON EXIT POPUP Command|ON PAD Command|  
|ON KEY LABEL Command|ON MACHELP Command|ON SELECTION BAR Command|  
|ON PAGE Command|ON READERROR Command|ON SELECTION POPUP Command|  
|ON SELECTION MENU Command|ON SELECTION PAD Command||  
|ON SHUTDOWN Command|OBJVAR( ) Function||  
  
## P  
  
||||  
|-|-|-|  
|_PADVANCE System Memory Variable|_PAGENO System Memory Variable|_PBPAGE System Memory Variable|  
|_PCOLNO System Memory Variable|_PCOPIES System Memory Variable|_PDRIVER System Memory Variable|  
|_PDSETUP System Memory Variable|_PECODE System Memory Variable|_PEJECT System Memory Variable|  
|_PEPAGE System Memory Variable|_PLENGTH System Memory Variable|_PLINENO System Memory Variable|  
|_PLOFFSET System Memory Variable|_PPITCH System Memory Variable|_PQUALITY System Memory Variable|  
|_PRETEXT System Memory Variable|_PSCODE System Memory Variable|_PSPACING System Memory Variable|  
|_PWAIT System Memory Variable|PACK DATABASE Command|PAD( ) Function|  
|PCOL( ) Function|PEMSTATUS( ) Function|PLAY MACRO Command|  
|POP KEY Command|POP MENU Command|POP POPUP Command|  
|POPUP( ) Function|PRINTJOB ... ENDPRINTJOB Command|PRINTSTATUS( ) Function|  
|PRMBAR( ) Function|PRMPAD( ) Function|PROMPT( ) Function|  
|PROW( ) Function|PRTINFO( ) Function|PUSH KEY Command|  
|PUSH MENU Command|PUSH POPUP Command|PUTFILE( ) Function|  
  
## Q  
  
||||  
|-|-|-|  
|QUIT Command|||  
  
## R  
  
||||  
|-|-|-|  
|_RMARGIN System Memory Variable|RD Command|READKEY( ) Function|  
|READ Command|READ MENU Command|RELEASE BAR Command|  
|REFRESH() Function|REINDEX Command|RELEASE LIBRARY Command|  
|RELEASE CLASSLIB Command|RELEASE Command|RELEASE PAD Command|  
|RELEASE MENUS Command|RELEASE MODULE Command|RELEASE WINDOWS Command|  
|RELEASE POPUPS Command|RELEASE PROCEDURE Command|RENAME Command|  
|REMOVE CLASS Command|RENAME CLASS Command|RENAME VIEW Command|  
|RENAME CONNECTION Command|RENAME TABLE Command|RESTORE FROM Command|  
|REPORT Command|REQUERY( ) Function|RESTORE WINDOW Command|  
|RESTORE MACROS Command|RESTORE SCREEN Command|RGBSCHEME( ) Function|  
|RESUME Command|RGB( ) Function|RUN &#124; ! Command|  
|RMDIR Command|ROW( ) Function||  
|RUNSCRIPT Command|RDLEVEL( ) Function||  
  
## S  
  
||||  
|-|-|-|  
|SAVE MACROS Command|SAVE SCREEN Command|SAVE TO Command|  
|SAVE WINDOWS Command|SCHEME( ) Function|SCOLS( ) Function|  
|SCROLL Command|_SCREEN System Memory Variable|SET Command|  
|SET ALTERNATE Command|SET ANSI Command|SET APLABOUT Command|  
|SET AUTOSAVE Command|SET BELL Command|SET BLINK Command|  
|SET BORDER Command|SET BRSTATUS Command|SET CLASSLIB Command|  
|SET CLEAR Command|SET CLOCK Command|SET COLOR OF Command|  
|SET COLOR OF SCHEME Command|SET COLOR SET Command|SET COLOR TO Command|  
|SET COMPATIBLE Command|SET CONFIRM Command|SET CONSOLE Command|  
|SET CPCOMPILE|SET CPDIALOG|SET CURRENCY Command|  
|SET CURSOR Command|SET DATASESSION Command|SET DEBUG Command|  
|SET DECIMALS Command|SET DELIMITERS Command|SET DEVELOPMENT Command|  
|SET DEVICE Command|SET DISPLAY Command|SET DOHISTORY Command|  
|SET ECHO Command|SET ESCAPE Command|SET FORMAT Command|  
|SET FUNCTION Command|SET HEADINGS Command|SET HELP Command|  
|SET HELPFILTER Command|SET INTENSITY Command|SET KEY Command|  
|SET KEYCOMP Command|SET LOGERRORS Command|SET MACDESKTOP Command|  
|SET MACHELP Command|SET MACKEY Command|SET MARGIN Command|  
|SET MARK OF Command|SET MARK TO Command|SET MEMOWIDTH Command|  
|SET MESSAGE Command|SET MOUSE Command|SET ODOMETER Command|  
|SET OLEOBJECT Command|SET PALETTE Command|SET PDSETUP Command|  
|SET POINT Command|SET PRINTER Command|SET READBORDER Command|  
|SET REFRESH Command|SET RESOURCE Command|SET SAFETY Command|  
|SET SCOREBOARD Command|SET SECONDS Command|SET SEPARATOR Command|  
|SET SHADOWS Command|SET SKIP OF Command|SET SPACE Command|  
|SET STATUS Command|SET STATUS BAR Command|SET STEP Command|  
|SET STICKY Command|SET SYSFORMATS Command|SET SYSMENU Command|  
|SET TALK Command|SET TEXTMERGE Command|SET TEXTMERGE DELIMITERS Command|  
|SET TOPIC Command|SET TOPIC ID Command|SET TRBETWEEN Command|  
|SET TYPEAHEAD Command|SET VIEW Command|SET WINDOW OF MEMO Command|  
|SET XCMDFILE Command|_SHELL System Memory Variable|SHOW GET Command|  
|SHOW GETS Command|SHOW MENU Command|SHOW OBJECT Command|  
|SHOW POPUP Command|SHOW WINDOW Command|SIZE POPUP Command|  
|SIZE WINDOW Command|SKPBAR( ) Function|SKPPAD( ) Function|  
|SOUNDEX( ) Function|_SPELLCHK System Memory Variable|SQL functions|  
|SROWS( ) Function|_STARTUP System Memory Variable|SUSPEND Command|  
|SYS() Functions except SYS(2011)|SYSMETRIC( ) Function||  
  
## T  
  
||||  
|-|-|-|  
|_TABS System Memory Variable|TEXT ... ENDTEXT Command|TXTWIDTH( ) Function|  
|TRANSFORM( ) Function|_TRANSPORT System Memory Variable||  
|TYPE Command|_THROTTLE System Memory Variable||  
  
## U  
  
||||  
|-|-|-|  
|UPDATED( ) Function|USE Command||  
  
## V  
  
||||  
|-|-|-|  
|VALIDATE DATABASE Command|VARREAD( ) Function|VERSION( ) Function|  
  
## W  
  
||||  
|-|-|-|  
|_WINDOWS System Memory Variable|_WIZARD System Memory Variable|WCHILD( ) Function|  
|WAIT Command|WBORDER( ) Function|WFONT( ) Function|  
|WCOLS( ) Function|WEXIST( ) Function|WLROW( ) Function|  
|WITH ... ENDWITH Command|WLAST( ) Function|WONTOP( ) Function|  
|WMAXIMUM( ) Function|WLCOL( ) Function|WREAD( ) Function|  
|WOUTPUT( ) Function|WMINIMUM( ) Function|WVISIBLE( ) Function|  
|WPARENT( ) Function|WTITLE( ) Function||  
|WROWS( ) Function|_WRAP System Memory Variable||  
  
## Z  
  
||||  
|-|-|-|  
|ZOOM WINDOW Command|||
