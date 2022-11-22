---
description: "Support for Rules, Triggers, Default Values, and Stored Procedures (Visual FoxPro ODBC Driver)"
title: "Support for Rules, Triggers, Default Values, and Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual FoxPro ODBC driver [ODBC], stored procedures"
  - "FoxPro ODBC driver [ODBC], commands and functions"
  - "commands for FoxPro ODBC driver [ODBC]"
  - "FoxPro ODBC driver [ODBC], default values"
  - "FoxPro ODBC driver [ODBC], triggers"
  - "stored procedures [ODBC], Visual FoxPro ODBC driver"
  - "Visual FoxPro ODBC driver [ODBC], rules"
  - "FoxPro commands and functions [ODBC]"
  - "rules [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], default values"
  - "FoxPro ODBC driver [ODBC], rules"
  - "functions [ODBC], Visual FoxPro"
  - "default values [ODBC]"
  - "Visual FoxPro ODBC driver [ODBC], commands and functions"
  - "Visual FoxPro ODBC driver [ODBC], triggers"
  - "FoxPro ODBC driver [ODBC], stored procedures"
  - "Visual FoxPro commands and functions [ODBC]"
ms.assetid: e449de20-d6ca-4902-9f8e-814eb6e86650
author: David-Engel
ms.author: v-davidengel
---
# Support for Rules, Triggers, Default Values, and Stored Procedures (Visual FoxPro ODBC Driver)
You cannot create Visual FoxPro rules, triggers, default values, or stored procedures using the Visual FoxPro ODBC Driver. However, your application might interact with existing rules, triggers, default values, or stored procedures as it inserts, updates, or deletes Visual FoxPro data stored in a database.  
  
 The following table lists the Visual FoxPro commands and functions supported by the Visual FoxPro ODBC Driver when the commands or functions exist in rules, triggers, default values, or stored procedures.  
  
 If your application interacts with data whose rules, triggers, default values, or stored procedures call any other Visual FoxPro commands or functions, the driver generates an error. See [Unsupported Visual FoxPro Commands and Functions](../../odbc/microsoft/unsupported-visual-foxpro-commands-and-functions-visual-foxpro-odbc-driver.md) for a list of commands and functions not supported by the driver.  
  
> [!TIP]  
>  If you want to insert conditional code into your rules, triggers, or stored procedures that determines the commands to execute when called by the driver, you can use the **VERSION( )** function. The **VERSION( )** function returns "Visual FoxPro ODBC Driver *\<version>*" when called by the driver.  
  
## Visual FoxPro Commands and Functions Supported in Rules, Triggers, Default Values, and Stored Procedures  

:::row:::
    :::column:::
        $ Operator  
        % Operator  
    :::column-end:::
    :::column:::
        & Command  
        && Command  
    :::column-end:::
    :::column:::
        \* Command  
        = Command  
    :::column-end:::
:::row-end:::

## A  

:::row:::
    :::column:::
        ABS( ) Function  
        ACOPY( ) Function  
        ACOS( ) Function  
        ADATABASES( ) Function  
        ADBOBJECTS( ) Function  
        ADD TABLE Command  
        ADEL( ) Function  
        AELEMENT( ) Function  
        AERROR( ) Function  
        AFIELDS( ) Function  
        AINS( ) Function  
        ALEN( ) Function  
        ALIAS( ) Function  
    :::column-end:::
    :::column:::
        ALLTRIM( ) Function  
        ALTER TABLE - SQL Command  
        AND Operator  
        APPEND Command  
        APPEND FROM Command  
        APPEND FROM ARRAY Command  
        APPEND GENERAL Command  
        APPEND MEMO Command  
        APPEND PROCEDURES Command  
        ASC( ) Function  
        ASCAN( ) Function  
        ASIN( ) Function  
        ASORT( ) Function  
    :::column-end:::
    :::column:::
        ASUBSCRIPT( ) Function  
        AT( ) Function  
        AT_C( ) Function  
        ATAN( ) Function  
        ATC( ) Function  
        ATCC( ) Function  
        ATCLINE( ) Function  
        ATLINE( ) Function  
        ATN2( ) Function  
        AUSED( ) Function  
        AVERAGE Command  
    :::column-end:::
:::row-end:::

## B  

:::row:::
    :::column:::
        BEGIN TRANSACTION Command  
        BETWEEN( ) Function  
        BITAND( ) Function  
        BITCLEAR( ) Function  
        BITLSHIFT( ) Function  
    :::column-end:::
    :::column:::
        BITNOT( ) Function  
        BITOR( ) Function  
        BITRSHIFT( ) Function  
        BITSET( ) Function  
        BITTEST( ) Function  
    :::column-end:::
    :::column:::
        BITXOR( ) Function  
        BLANK Command  
        BOF( ) Function  
    :::column-end:::
:::row-end:::

## C  

:::row:::
    :::column:::
        CALCULATE Command  
        CANDIDATE( ) Function  
        CDOW( ) Function  
        CDX( ) Function  
        CEILING( ) Function  
        CHR( ) Function  
        CHRTRAN( ) Function  
        CHRTRANC( ) Function  
        CLOSE Commands  
        CMONTH( ) Function  
    :::column-end:::
    :::column:::
        CONTINUE Command  
        COPY INDEXES Command  
        COPY PROCEDURES Command  
        COPY STRUCTURE Command  
        COPY STRUCTURE EXTENDED Command  
        COPY TAG Command  
        COPY TO Command  
        COPY TO ARRAY Command  
        COS( ) Function  
        COUNT Command  
    :::column-end:::
    :::column:::
        CPCONVERT( ) Function  
        CPCURRENT( ) Function  
        CPDBF( ) Function  
        CTOD( ) Function  
        CTOT( ) Function  
        CURSORGETPROP( ) Function  
        CURSORSETPROP( ) Function  
        CURVAL( ) Function  
    :::column-end:::
:::row-end:::

## D  

:::row:::
    :::column:::
        DATE( ) Function  
        DATETIME( ) Function  
        DAY( ) Function  
        DBC( ) Function  
        DBF( ) Function  
        DBGETPROP( ) Function  
        DBUSED( ) Function  
        DELETE - SQL Command  
    :::column-end:::
    :::column:::
        DELETE Command  
        DELETE TAG Command  
        DELETED( ) Function  
        DESCENDING( ) Function  
        DIFFERENCE( ) Function  
        DIMENSION Command  
        DISKSPACE( ) Function  
        DMY( ) Function  
    :::column-end:::
    :::column:::
        DO Command  
        DO CASE ... ENDCASE Command  
        DO WHILE ... ENDDO Command  
        DOW( ) Function  
        DTOC( ) Function  
        DTOR( ) Function  
        DTOS( ) Function  
        DTOT( ) Function  
    :::column-end:::
:::row-end:::

## E  

:::row:::
    :::column:::
        EMPTY( ) Function  
        END TRANSACTION Command  
        EOF( ) Function  
    :::column-end:::
    :::column:::
        ERROR( ) Function  
        EVALUATE( ) Function  
        EXIT Command  
    :::column-end:::
    :::column:::
        EXP( ) Function  
    :::column-end:::
:::row-end:::

## F  

:::row:::
    :::column:::
        FCOUNT( ) Function  
        FDATE( ) Function  
        FIELD( ) Function  
        FILE( ) Function  
        FILTER( ) Function  
        FLDLIST( ) Function  
    :::column-end:::
    :::column:::
        FLOCK( ) Function  
        FLOOR( ) Function  
        FLUSH Command  
        FOR( ) Function  
        FOR ... ENDFOR Command  
        FOUND( ) Function  
    :::column-end:::
    :::column:::
        FREE TABLE Command  
        FSIZE( ) Function  
        FTIME( ) Function  
        FULLPATH( ) Function  
        FUNCTION Command  
        FV( ) Function  
    :::column-end:::
:::row-end:::

## G  

:::row:::
    :::column:::
        GATHER Command  
        GETCP( ) Function  
        GETENV( ) Function  
    :::column-end:::
    :::column:::
        GETFLDSTATE( ) Function  
        GETNEXTMODIFIED( ) Function  
        GO/GOTO Command  
    :::column-end:::
    :::column:::
        GOMONTH( ) Function  
    :::column-end:::
:::row-end:::

## H  

:::row:::
    :::column:::
        HEADER( ) Function
    :::column-end:::
    :::column:::
        HOUR( ) Function
    :::column-end:::
:::row-end:::

## I  

:::row:::
    :::column:::
        IDXCOLLATE( ) Function  
        IF ... ENDIF Command  
        IIF( ) Function  
        INDBC( ) Function  
        INDEX Command  
        INLIST( ) Function  
    :::column-end:::
    :::column:::
        INSERT-SQL Command  
        INT( ) Function  
        ISALPHA( ) Function  
        ISBLANK( ) Function  
        ISDIGIT( ) Function  
        ISEXCLUSIVE( ) Function  
    :::column-end:::
    :::column:::
        ISLEADBYTE( ) Function  
        ISLOWER( ) Function  
        ISNULL( ) Function  
        ISREADONLY( ) Function  
        ISUPPER( ) Function  
    :::column-end:::
:::row-end:::

## K  

:::row:::
    :::column:::
        KEY( ) Function
    :::column-end:::
    :::column:::
        KEYMATCH( ) Function
    :::column-end:::
:::row-end:::

## L  

:::row:::
    :::column:::
        LEFT( ) Function  
        LEFTC( ) Function  
        LEN( ) Function  
        LENC( ) Function  
        LIKE( ) Function  
        LIKEC( ) Function  
    :::column-end:::
    :::column:::
        LOCAL Command  
        LOCATE Command  
        LOCK( ) Function  
        LOG( ) Function  
        LOG10( ) Function  
        LOOKUP( ) Function  
    :::column-end:::
    :::column:::
        LOWER( ) Function  
        LPARAMETERS Command  
        LTRIM( ) Function  
        LUPDATE( ) Function  
    :::column-end:::
:::row-end:::

## M  

:::row:::
    :::column:::
        MAX( ) Function  
        MDX( ) Function  
        MDY( ) Function  
        MEMLINES( ) Function  
    :::column-end:::
    :::column:::
        MESSAGE( ) Function  
        MIN( ) Function  
        MINUTE( ) Function  
        _MLINE System Memory Variable  
    :::column-end:::
    :::column:::
        MLINE( ) Function  
        MOD( ) Function  
        MONTH( ) Function  
        MTON( ) Function  
    :::column-end:::
:::row-end:::

## N  

:::row:::
    :::column:::
        NDX( ) Function  
        NORMALIZE( ) Function  
    :::column-end:::
    :::column:::
        NOT Operator  
        NOTE Command  
    :::column-end:::
    :::column:::
        NTOM( ) Function  
        NVL( ) Function  
    :::column-end:::
:::row-end:::

## O  

:::row:::
    :::column:::
        OCCURS( ) Function  
        OLDVAL( ) Function  
        ON( ) Function  
    :::column-end:::
    :::column:::
        ON ERROR Command  
        ON KEY Command  
        OPEN DATABASE Command  
    :::column-end:::
    :::column:::
        OR Operator  
        ORDER( ) Function  
        OS( ) Function  
    :::column-end:::
:::row-end:::

## P  

:::row:::
    :::column:::
        PACK Command  
        PADL( ) &#124; PADR( ) &#124; PADC( ) Functions  
        PARAMETERS Command  
        PARAMETERS( ) Function  
        PAYMENT( ) Function  
    :::column-end:::
    :::column:::
        PI( ) Function  
        PRIMARY( ) Function  
        PRIVATE Command  
        PROCEDURE Command  
        PROGRAM( ) Function  
    :::column-end:::
    :::column:::
        PROPER( ) Function  
        PUBLIC Command  
        PV( ) Function  
    :::column-end:::
:::row-end:::

## R  

:::row:::
    :::column:::
        RAND( ) Function  
        RAT( ) Function  
        RATC( ) Function  
        RATLINE( ) Function  
        RECALL Command  
        RECCOUNT( ) Function  
        RECNO( ) Function  
        RECSIZE( ) Function  
    :::column-end:::
    :::column:::
        REGIONAL Command  
        RELATION( ) Function  
        REMOVE TABLE Command  
        REPLACE Command  
        REPLACE FROM ARRAY Command  
        REPLICATE( ) Function  
        RETRY Command  
        RETURN Command  
    :::column-end:::
    :::column:::
        RIGHT( ) Function  
        RIGHTC( ) Function  
        RLOCK( ) Function  
        ROLLBACK Command  
        ROUND( ) Function  
        RTOD( ) Function  
        RTRIM( ) Function  
    :::column-end:::
:::row-end:::

## S  

:::row:::
    :::column:::
        SCAN ... ENDSCAN Command  
        SCATTER Command  
        SEC( ) Function  
        SECONDS( ) Function  
        SEEK Command  
        SEEK( ) Function  
        SELECT Command  
        SELECT( ) Function  
        SELECT-SQL Command  
        SET( ) Function  
        SET BLOCKSIZE Command  
        SET CARRY Command  
        SET CENTURY Command  
        SET COLLATE Command  
        SET DATABASE Command  
        SET DATE Command  
        SET DEFAULT Command  
        SET DELETED Command  
        SET EXACT Command  
        SET EXCLUSIVE Command  
        SET FDOW Command  
    :::column-end:::
    :::column:::
        SET FIELDS Command  
        SET FILTER Command  
        SET FIXED Command  
        SET FULLPATH Command  
        SET FWEEK Command  
        SET HOURS Command  
        SET INDEX Command  
        SET LOCK Command  
        SET MULTILOCKS Command  
        SET NEAR Command  
        SET NOCPTRANS Command  
        SET NOTIFY Command  
        SET NULL Command  
        SET OPTIMIZE Command  
        SET ORDER Command  
        SET PATH Command  
        SET PROCEDURE Command  
        SET RELATION Command  
        SET RELATION OFF Command  
        SET REPROCESS Command  
        SET SKIP Command  
    :::column-end:::
    :::column:::
        SET UDFPARMS Command  
        SET UNIQUE Command  
        SET VOLUME Command  
        SETFLDSTATE( ) Function  
        SIGN ( ) Function  
        SIN( ) Function  
        SKIP Command  
        SORT Command  
        SPACE( ) Function  
        SQRT( ) Function  
        STORE Command  
        STR( ) Function  
        STRCONV( ) Function  
        STRTRAN( ) Function  
        STUFF( ) Function  
        STUFFC( ) Function  
        SUBSTR( ) Function  
        SUBSTRC( ) Function  
        SUM Command  
        SYS(2011) Function  
    :::column-end:::
:::row-end:::

## T  

:::row:::
    :::column:::
        TABLEREVERT( ) Function  
        TABLEUPDATE( ) Function  
        TAG( ) Function  
        TAGCOUNT( ) Function  
        TAGNO( ) Function  
        _TALLY System Memory Variable  
    :::column-end:::
    :::column:::
        TAN( ) Function  
        TARGET( ) Function  
        TIME( ) Function  
        TOTAL Command  
        _TRIGGERLEVEL System Memory Variable  
        TRIM( ) Function  
    :::column-end:::
    :::column:::
        TTOC( ) Function  
        TTOD( ) Function  
        TXNLEVEL( ) Function  
        TYPE( ) Function  
    :::column-end:::
:::row-end:::

## U  

:::row:::
    :::column:::
        UNIQUE( ) Function  
        UNLOCK Command  
        UPDATE Command  
    :::column-end:::
    :::column:::
        UPDATE - SQL Command  
        UPPER( ) Function  
        USE Command  
    :::column-end:::
    :::column:::
        USED( ) Function  
    :::column-end:::
:::row-end:::

## V  

:::row:::
    :::column:::
        VAL( ) Function
    :::column-end:::
    :::column:::
        VERSION( ) Function
    :::column-end:::
:::row-end:::

## W  

WEEK( ) Function

## Y  

YEAR( ) Function

## Z  

ZAP Command
