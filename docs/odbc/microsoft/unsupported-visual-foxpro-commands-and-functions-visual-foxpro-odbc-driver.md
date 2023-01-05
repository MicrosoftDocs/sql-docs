---
description: "Unsupported Visual FoxPro Commands and Functions (Visual FoxPro ODBC Driver)"
title: "Unsupported Visual FoxPro Commands and Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "FoxPro ODBC driver [ODBC], commands and functions"
  - "functions [ODBC], Visual FoxPro"
  - "Visual FoxPro ODBC driver [ODBC], commands and functions"
  - "Visual FoxPro commands and functions"
  - "FoxPro ODBC driver"
ms.assetid: afdb6b7e-738d-42ca-8053-67ae50873ca6
author: David-Engel
ms.author: v-davidengel
---
# Unsupported Visual FoxPro Commands and Functions (Visual FoxPro ODBC Driver)
The following table lists FoxPro commands and functions that are not supported by the Visual FoxPro ODBC Driver but are supported by Microsoft® Visual FoxPro®.  
  
 If your application interacts with data whose rules, triggers, default values, or stored procedures call these Visual FoxPro commands or functions, the driver can generate an error.  
  
## Unsupported Visual FoxPro Commands and Functions  

:::row:::
    :::column:::
        ! Command (see RUN &#124; ! Command)  
        #DEFINE ... #UNDEF  
        #IF ... #ENDIF Preprocessor Directive  
        #IFDEF &#124; #IFNDEF  
        #INCLUDE Preprocessor Directive  
        :: Scope Resolution Operator  
        ? &#124; ?? Command  
    :::column-end:::
    :::column:::
        ??? Command  
        @ ... BOX Command  
        @ ... CLASS Command  
        @ ... CLEAR Command  
        @ ... EDIT - Edit Boxes Command  
        @ ... FILL Command  
        @ ... GET  
    :::column-end:::
    :::column:::
        @ ... MENU Command  
        @ ... PROMPT Command  
        @ ... SAY Command  
        @ ... SCROLL Command  
        @ ... TO Command  
        \ &#124; \\\ Command  
    :::column-end:::
:::row-end:::

## A  

:::row:::
    :::column:::
        ACCEPT Command  
        ACLASS( ) Function  
        ACTIVATE MENU Command  
        ACTIVATE POPUP Command  
        ACTIVATE SCREEN Command  
        ACTIVATE WINDOW Command  
    :::column-end:::
    :::column:::
        ActivateCell Method  
        ADD CLASS Command  
        ADIR( ) Function  
        AFONT( ) Function  
        AINSTANCE( ) Function  
        _ALIGNMENT System Memory Variable  
    :::column-end:::
    :::column:::
        AMEMBERS( ) Function  
        ANSITOOEM( ) Function  
        APRINTERS( ) Function  
        ASELOBJ( ) Function  
        ASSIST Command  
    :::column-end:::
:::row-end:::

## B  

:::row:::
    :::column:::
        BAR( ) Function  
        BARCOUNT( ) Function  
        BARPROMPT( ) Function  
        _BEAUTIFY System Memory Variable  
    :::column-end:::
    :::column:::
        _BOX System Memory Variable  
        BROWSE Command  
        _BROWSER System Memory Variable  
        BUILD APP Command  
    :::column-end:::
    :::column:::
        BUILD EXE Command  
        BUILD PROJECT Command  
        _BUILDER System Memory Variable  
    :::column-end:::
:::row-end:::

## C  

:::row:::
    :::column:::
        _CALCVALUE System Memory Variable  
        CALL Command  
        CANCEL Command  
        CAPSLOCK( ) Function  
        CD Command  
        CHANGE Command  
        CHDIR Command  
        CHRSAW( ) Function  
        _CLIPTEXT System Memory Variable  
        CLOSE MEMO Command  
        CNTBAR( ) Function  
        CNTPAD( ) Function  
        COL( ) Function  
        COMPILE Command  
    :::column-end:::
    :::column:::
        COMPILE DATABASE Command  
        COMPILE FORM Command  
        COMPOBJ( ) Function  
        Container Object  
        Control Object  
        _CONVERTER System Memory Variable  
        COPY FILE Command  
        COPY MEMO Command  
        CREATE Command  
        CREATE CLASS Command  
        CREATE CLASSLIB Command  
        CREATE COLOR SET Command  
        CREATE CONNECTION Command  
        CREATE DATABASE Command  
    :::column-end:::
    :::column:::
        CREATE FORM Command  
        CREATE FROM Command  
        CREATE LABEL Command  
        CREATE MENU Command  
        CREATE PROJECT Command  
        CREATE QUERY Command  
        CREATE REPORT Command  
        CREATE SCREEN Command  
        CREATE SQL VIEW Command  
        CREATE TRIGGER Command  
        CREATE VIEW Command  
        CREATEOBJECT( ) Function  
        CURDIR( ) Function  
        _CUROBJ System Memory Variable  
    :::column-end:::
:::row-end:::

## D  

:::row:::
    :::column:::
        _DBLCLICK System Memory Variable  
        DBSETPROP( ) Function  
        DDE Functions  
        DEACTIVATE MENU Command  
        DEACTIVATE POPUP Command  
        DEACTIVATE WINDOW Command  
        DECLARE Command  
        DECLARE - DLL Command  
        DEFINE BAR Command  
        DEFINE BOX Command  
        DEFINE CLASS Command  
        DEFINE MENU Command  
    :::column-end:::
    :::column:::
        DEFINE PAD Command  
        DEFINE POPUP Command  
        DEFINE WINDOW Command  
        DELETE CONNECTION Command  
        DELETE DATABASE Command  
        DELETE FILE Command  
        DELETE TRIGGER Command  
        DELETE VIEW Command  
        _DIARYDATE System Memory Variable  
        DIR Command  
        DIRECTORY Command  
        DISPLAY Command  
    :::column-end:::
    :::column:::
        DISPLAY CONNECTIONS Command  
        DISPLAY DATABASE Command  
        DISPLAY DLLS Command  
        DISPLAY FILES Command  
        DISPLAY MEMORY Command  
        DISPLAY OBJECTS Command  
        DISPLAY PROCEDURES Command  
        DISPLAY STATUS Command  
        DISPLAY STRUCTURE Command  
        DISPLAY TABLES Command  
        DISPLAY VIEWS Command  
        DO FORM Command  
    :::column-end:::
:::row-end:::

## E  

:::row:::
    :::column:::
        EDIT Command  
        EJECT Command  
        EJECT PAGE Command  
    :::column-end:::
    :::column:::
        ERASE Command  
        ERROR Command  
        EXPORT Command  
    :::column-end:::
    :::column:::
        EXTERNAL Command  
    :::column-end:::
:::row-end:::

## F  

:::row:::
    :::column:::
        FCHSIZE( ) Function  
        FCLOSE( ) Function  
        FCREATE( ) Function  
        FEOF( ) Function  
        FERROR( ) Function  
        FFLUSH( ) Function  
        FGETS( ) Function  
    :::column-end:::
    :::column:::
        FILER Command  
        FIND Command  
        FKLABEL( ) Function  
        FKMAX( ) Function  
        FONTMETRIC( ) Function  
        FOPEN( ) Function  
        _FOXDOC System Memory Variable  
    :::column-end:::
    :::column:::
        _FOXGRAPH System Memory Variable  
        FPUTS( ) Function  
        FREAD( ) Function  
        FSEEK( ) Function  
        FWRITE( ) Function  
    :::column-end:::
:::row-end:::

## G  

:::row:::
    :::column:::
        _GENGRAPH System Memory Variable  
        _GENMENU System Memory Variable  
        _GENPD System Memory Variable  
        _GENSCRN System Memory Variable  
        _GENXTAB System Memory Variable  
    :::column-end:::
    :::column:::
        GETBAR( ) Function  
        GETCOLOR( ) Function  
        GETDIR( ) Function  
        GETEXPR Command  
        GETFILE( ) Function  
    :::column-end:::
    :::column:::
        GETFONT( ) Function  
        GETOBJECT( ) Function  
        GETPAD( ) Function  
        GETPICT( ) Function  
        GETPRINTER( ) Function  
    :::column-end:::
:::row-end:::

## H  

:::row:::
    :::column:::
        HELP Command  
        HIDE MENU Command  
    :::column-end:::
    :::column:::
        HIDE POPUP Command  
        HIDE WINDOW Command  
    :::column-end:::
    :::column:::
        HOME( ) Function  
    :::column-end:::
:::row-end:::

## I  

:::row:::
    :::column:::
        IMESTATUS( ) Function  
        IMPORT Command  
        _INDENT System Memory Variable  
        INDEX ON Command  
    :::column-end:::
    :::column:::
        INKEY( ) Function  
        INPUT Command  
        INSERT Command  
        INSMODE( ) Function  
    :::column-end:::
    :::column:::
        ISCOLOR( ) Function  
        ISMOUSE( ) Function  
    :::column-end:::
:::row-end:::

## J  

JOIN Command

## K  

KEYBOARD Command

## L  

:::row:::
    :::column:::
        LABEL Command  
        LASTKEY( ) Function  
        LINENO( ) Function  
    :::column-end:::
    :::column:::
        LIST Commands  
        LIST CONNECTIONS Command  
        _LMARGIN System Memory Variable  
    :::column-end:::
    :::column:::
        LOAD Command  
        LOCFILE( ) Function  
    :::column-end:::
:::row-end:::

## M  

:::row:::
    :::column:::
        MCOL( ) Function  
        MD Command  
        MDOWN( ) Function  
        MEMORY( ) Function  
        MENU Command  
        MENU( ) Function  
        MENU TO Command  
        MESSAGEBOX( ) Function  
        MKDIR Command  
        MODIFY CLASS Command  
        MODIFY COMMAND Command  
        MODIFY CONNECTION Command  
    :::column-end:::
    :::column:::
        MODIFY DATABASE Command  
        MODIFY FILE Command  
        MODIFY FORM Command  
        MODIFY GENERAL Command  
        MODIFY LABEL Command  
        MODIFY MEMO Command  
        MODIFY MENU Command  
        MODIFY PROCEDURE Command  
        MODIFY PROJECT Command  
        MODIFY QUERY Command  
        MODIFY REPORT Command  
        MODIFY SCREEN Command  
    :::column-end:::
    :::column:::
        MODIFY STRUCTURE Command  
        MODIFY VIEW Command  
        MODIFY WINDOW Command  
        MOUSE Command  
        MOVE POPUP Command  
        MOVE WINDOW Command  
        MRKBAR( ) Function  
        MRKPAD( ) Function  
        MROW( ) Function  
        MWINDOW( ) Function  
    :::column-end:::
:::row-end:::

## N  

NUMLOCK( ) Function

## O  

:::row:::
    :::column:::
        OBJNUM( ) Function  
        OBJTOCLIENT( ) Function  
        OBJVAR( ) Function  
        OEMTOANSI( ) Function  
        ON APLABOUT Command  
        ON BAR Command  
        ON ESCAPE Command  
        ON EXIT BAR Command  
    :::column-end:::
    :::column:::
        ON EXIT MENU Command  
        ON EXIT PAD Command  
        ON EXIT POPUP Command  
        ON KEY = Command  
        ON KEY LABEL Command  
        ON MACHELP Command  
        ON PAD Command  
        ON PAGE Command  
    :::column-end:::
    :::column:::
        ON READERROR Command  
        ON SELECTION BAR Command  
        ON SELECTION MENU Command  
        ON SELECTION PAD Command  
        ON SELECTION POPUP Command  
        ON SHUTDOWN Command  
    :::column-end:::
:::row-end:::

## P  

:::row:::
    :::column:::
        PACK DATABASE Command  
        PAD( ) Function  
        _PADVANCE System Memory Variable  
        _PAGENO System Memory Variable  
        _PBPAGE System Memory Variable  
        PCOL( ) Function  
        _PCOLNO System Memory Variable  
        _PCOPIES System Memory Variable  
        _PDRIVER System Memory Variable  
        _PDSETUP System Memory Variable  
        _PECODE System Memory Variable  
        _PEJECT System Memory Variable  
        PEMSTATUS( ) Function  
    :::column-end:::
    :::column:::
        _PEPAGE System Memory Variable  
        PLAY MACRO Command  
        _PLENGTH System Memory Variable  
        _PLINENO System Memory Variable  
        _PLOFFSET System Memory Variable  
        POP KEY Command  
        POP MENU Command  
        POP POPUP Command  
        POPUP( ) Function  
        _PPITCH System Memory Variable  
        _PQUALITY System Memory Variable  
        _PRETEXT System Memory Variable  
        PRINTJOB ... ENDPRINTJOB Command  
    :::column-end:::
    :::column:::
        PRINTSTATUS( ) Function  
        PRMBAR( ) Function  
        PRMPAD( ) Function  
        PROMPT( ) Function  
        PROW( ) Function  
        PRTINFO( ) Function  
        _PSCODE System Memory Variable  
        _PSPACING System Memory Variable  
        PUSH KEY Command  
        PUSH MENU Command  
        PUSH POPUP Command  
        PUTFILE( ) Function  
        _PWAIT System Memory Variable  
    :::column-end:::
:::row-end:::

## Q  

QUIT Command

## R  

:::row:::
    :::column:::
        RD Command  
        RDLEVEL( ) Function  
        READ Command  
        READ MENU Command  
        READKEY( ) Function  
        REFRESH() Function  
        REINDEX Command  
        RELEASE Command  
        RELEASE BAR Command  
        RELEASE CLASSLIB Command  
        RELEASE LIBRARY Command  
        RELEASE MENUS Command  
        RELEASE MODULE Command  
    :::column-end:::
    :::column:::
        RELEASE PAD Command  
        RELEASE POPUPS Command  
        RELEASE PROCEDURE Command  
        RELEASE WINDOWS Command  
        REMOVE CLASS Command  
        RENAME Command  
        RENAME CLASS Command  
        RENAME CONNECTION Command  
        RENAME TABLE Command  
        RENAME VIEW Command  
        REPORT Command  
        REQUERY( ) Function  
        RESTORE FROM Command  
    :::column-end:::
    :::column:::
        RESTORE MACROS Command  
        RESTORE SCREEN Command  
        RESTORE WINDOW Command  
        RESUME Command  
        RGB( ) Function  
        RGBSCHEME( ) Function  
        _RMARGIN System Memory Variable  
        RMDIR Command  
        ROW( ) Function  
        RUN &#124; ! Command  
        RUNSCRIPT Command  
    :::column-end:::
:::row-end:::

## S  

:::row:::
    :::column:::
        SAVE MACROS Command  
        SAVE SCREEN Command  
        SAVE TO Command  
        SAVE WINDOWS Command  
        SCHEME( ) Function  
        SCOLS( ) Function  
        _SCREEN System Memory Variable  
        SCROLL Command  
        SET Command  
        SET ALTERNATE Command  
        SET ANSI Command  
        SET APLABOUT Command  
        SET AUTOSAVE Command  
        SET BELL Command  
        SET BLINK Command  
        SET BORDER Command  
        SET BRSTATUS Command  
        SET CLASSLIB Command  
        SET CLEAR Command  
        SET CLOCK Command  
        SET COLOR OF Command  
        SET COLOR OF SCHEME Command  
        SET COLOR SET Command  
        SET COLOR TO Command  
        SET COMPATIBLE Command  
        SET CONFIRM Command  
        SET CONSOLE Command  
        SET CPCOMPILE  
        SET CPDIALOG  
        SET CURRENCY Command  
        SET CURSOR Command  
        SET DATASESSION Command  
        SET DEBUG Command  
        SET DECIMALS Command  
        SET DELIMITERS Command  
        SET DEVELOPMENT Command  
        SET DEVICE Command  
    :::column-end:::
    :::column:::
        SET DISPLAY Command  
        SET DOHISTORY Command  
        SET ECHO Command  
        SET ESCAPE Command  
        SET FORMAT Command  
        SET FUNCTION Command  
        SET HEADINGS Command  
        SET HELP Command  
        SET HELPFILTER Command  
        SET INTENSITY Command  
        SET KEY Command  
        SET KEYCOMP Command  
        SET LOGERRORS Command  
        SET MACDESKTOP Command  
        SET MACHELP Command  
        SET MACKEY Command  
        SET MARGIN Command  
        SET MARK OF Command  
        SET MARK TO Command  
        SET MEMOWIDTH Command  
        SET MESSAGE Command  
        SET MOUSE Command  
        SET ODOMETER Command  
        SET OLEOBJECT Command  
        SET PALETTE Command  
        SET PDSETUP Command  
        SET POINT Command  
        SET PRINTER Command  
        SET READBORDER Command  
        SET REFRESH Command  
        SET RESOURCE Command  
        SET SAFETY Command  
        SET SCOREBOARD Command  
        SET SECONDS Command  
        SET SEPARATOR Command  
        SET SHADOWS Command  
        SET SKIP OF Command  
    :::column-end:::
    :::column:::
        SET SPACE Command  
        SET STATUS Command  
        SET STATUS BAR Command  
        SET STEP Command  
        SET STICKY Command  
        SET SYSFORMATS Command  
        SET SYSMENU Command  
        SET TALK Command  
        SET TEXTMERGE Command  
        SET TEXTMERGE DELIMITERS Command  
        SET TOPIC Command  
        SET TOPIC ID Command  
        SET TRBETWEEN Command  
        SET TYPEAHEAD Command  
        SET VIEW Command  
        SET WINDOW OF MEMO Command  
        SET XCMDFILE Command  
        _SHELL System Memory Variable  
        SHOW GET Command  
        SHOW GETS Command  
        SHOW MENU Command  
        SHOW OBJECT Command  
        SHOW POPUP Command  
        SHOW WINDOW Command  
        SIZE POPUP Command  
        SIZE WINDOW Command  
        SKPBAR( ) Function  
        SKPPAD( ) Function  
        SOUNDEX( ) Function  
        _SPELLCHK System Memory Variable  
        SQL functions  
        SROWS( ) Function  
        _STARTUP System Memory Variable  
        SUSPEND Command  
        SYS() Functions except SYS(2011)  
        SYSMETRIC( ) Function  
    :::column-end:::
:::row-end:::

## T  

:::row:::
    :::column:::
        _TABS System Memory Variable  
        TEXT ... ENDTEXT Command  
        _THROTTLE System Memory Variable  
    :::column-end:::
    :::column:::
        TRANSFORM( ) Function  
        _TRANSPORT System Memory Variable  
        TXTWIDTH( ) Function  
    :::column-end:::
    :::column:::
        TYPE Command  
    :::column-end:::
:::row-end:::

## U  

:::row:::
    :::column:::
        UPDATED( ) Function  
    :::column-end:::
    :::column:::
        USE Command  
    :::column-end:::
:::row-end:::

## V  

:::row:::
    :::column:::
        VALIDATE DATABASE Command  
    :::column-end:::
    :::column:::
        VARREAD( ) Function  
    :::column-end:::
    :::column:::
        VERSION( ) Function  
    :::column-end:::
:::row-end:::

## W  

:::row:::
    :::column:::
        WAIT Command  
        WBORDER( ) Function  
        WCHILD( ) Function  
        WCOLS( ) Function  
        WEXIST( ) Function  
        WFONT( ) Function  
        _WINDOWS System Memory Variable  
        WITH ... ENDWITH Command  
    :::column-end:::
    :::column:::
        _WIZARD System Memory Variable  
        WLAST( ) Function  
        WLCOL( ) Function  
        WLROW( ) Function  
        WMAXIMUM( ) Function  
        WMINIMUM( ) Function  
        WONTOP( ) Function  
        WOUTPUT( ) Function  
    :::column-end:::
    :::column:::
        WPARENT( ) Function  
        _WRAP System Memory Variable  
        WREAD( ) Function  
        WROWS( ) Function  
        WTITLE( ) Function  
        WVISIBLE( ) Function  
    :::column-end:::
:::row-end:::

## Z  

ZOOM WINDOW Command
