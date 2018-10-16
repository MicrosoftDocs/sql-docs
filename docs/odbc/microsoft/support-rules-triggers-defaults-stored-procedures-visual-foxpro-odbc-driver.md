---
title: "Support for Rules, Triggers, Default Values, and Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
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
author: MightyPen
ms.author: genemi
manager: craigg
---
# Support for Rules, Triggers, Default Values, and Stored Procedures (Visual FoxPro ODBC Driver)
You cannot create Visual FoxPro rules, triggers, default values, or stored procedures using the Visual FoxPro ODBC Driver. However, your application might interact with existing rules, triggers, default values, or stored procedures as it inserts, updates, or deletes Visual FoxPro data stored in a database.  
  
 The following table lists the Visual FoxPro commands and functions supported by the Visual FoxPro ODBC Driver when the commands or functions exist in rules, triggers, default values, or stored procedures.  
  
 If your application interacts with data whose rules, triggers, default values, or stored procedures call any other Visual FoxPro commands or functions, the driver generates an error. See [Unsupported Visual FoxPro Commands and Functions](../../odbc/microsoft/unsupported-visual-foxpro-commands-and-functions-visual-foxpro-odbc-driver.md) for a list of commands and functions not supported by the driver.  
  
> [!TIP]  
>  If you want to insert conditional code into your rules, triggers, or stored procedures that determines the commands to execute when called by the driver, you can use the **VERSION( )** function. The **VERSION( )** function returns "Visual FoxPro ODBC Driver *\<version>*" when called by the driver.  
  
## Visual FoxPro Commands and Functions Supported in Rules, Triggers, Default Values, and Stored Procedures  
  
||||  
|-|-|-|  
|$ Operator|% Operator|& Command|  
|&& Command|* Command|= Command|  
  
## A  
  
||||  
|-|-|-|  
|ABS( ) Function|ACOPY( ) Function|ADD TABLE Command|  
|ADATABASES( ) Function|ADBOBJECTS( ) Function|AERROR( ) Function|  
|ADEL( ) Function|AELEMENT( ) Function|ALEN( ) Function|  
|AFIELDS( ) Function|AINS( ) Function|ALTER TABLE - SQL Command|  
|ALIAS( ) Function|ALLTRIM( ) Function|APPEND FROM ARRAY Command|  
|AND Operator|APPEND Command|APPEND MEMO Command|  
|APPEND FROM Command|APPEND GENERAL Command|ASCAN( ) Function|  
|APPEND PROCEDURES Command|ASC( ) Function|ASUBSCRIPT( ) Function|  
|ASIN( ) Function|ASORT( ) Function|ATAN( ) Function|  
|AT( ) Function|AT_C( ) Function|ATCLINE( ) Function|  
|ATC( ) Function|ATCC( ) Function|AUSED( ) Function|  
|ATLINE( ) Function|ATN2( ) Function||  
|AVERAGE Command|ACOS( ) Function||  
  
## B  
  
||||  
|-|-|-|  
|BEGIN TRANSACTION Command|BETWEEN( ) Function|BITNOT( ) Function|  
|BITCLEAR( ) Function|BITLSHIFT( ) Function|BITSET( ) Function|  
|BITOR( ) Function|BITRSHIFT( ) Function|BLANK Command|  
|BITTEST( ) Function|BITXOR( ) Function||  
|BOF( ) Function|BITAND( ) Function||  
  
## C  
  
||||  
|-|-|-|  
|CALCULATE Command|CANDIDATE( ) Function|CHR( ) Function|  
|CDX( ) Function|CEILING( ) Function|CLOSE Commands|  
|CHRTRAN( ) Function|CHRTRANC( ) Function|COPY INDEXES Command|  
|CMONTH( ) Function|CONTINUE Command|COPY STRUCTURE EXTENDED Command|  
|COPY PROCEDURES Command|COPY STRUCTURE Command|COPY TO Command|  
|COPY TAG Command|COPY TO ARRAY Command|CPCONVERT( ) Function|  
|COS( ) Function|COUNT Command|CTOD( ) Function|  
|CPCURRENT( ) Function|CPDBF( ) Function|CURSORSETPROP( ) Function|  
|CTOT( ) Function|CURSORGETPROP( ) Function||  
|CURVAL( ) Function|CDOW( ) Function||  
  
## D  
  
||||  
|-|-|-|  
|DATE( ) Function|DATETIME( ) Function|DAY( ) Function|  
|DBC( ) Function|DBF( ) Function|DBGETPROP( ) Function|  
|DBUSED( ) Function|DELETE - SQL Command|DELETE Command|  
|DELETE TAG Command|DELETED( ) Function|DESCENDING( ) Function|  
|DIFFERENCE( ) Function|DIMENSION Command|DISKSPACE( ) Function|  
|DMY( ) Function|DO CASE ... ENDCASE Command|DO Command|  
|DO WHILE ... ENDDO Command|DOW( ) Function|DTOC( ) Function|  
|DTOR( ) Function|DTOS( ) Function|DTOT( ) Function|  
  
## E  
  
||||  
|-|-|-|  
|EMPTY( ) Function|EVALUATE( ) Function|EXIT Command|  
|ERROR( ) Function|EXP( ) Function||  
|END TRANSACTION Command|EOF( ) Function||  
  
## F  
  
||||  
|-|-|-|  
|FCOUNT( ) Function|FDATE( ) Function|FIELD( ) Function|  
|FILE( ) Function|FILTER( ) Function|FLDLIST( ) Function|  
|FLOCK( ) Function|FLOOR( ) Function|FLUSH Command|  
|FOR ... ENDFOR Command|FOR( ) Function|FOUND( ) Function|  
|FREE TABLE Command|FSIZE( ) Function|FTIME( ) Function|  
|FULLPATH( ) Function|FUNCTION Command|FV( ) Function|  
  
## G  
  
||||  
|-|-|-|  
|GATHER Command|GETNEXTMODIFIED( ) Function|GO/GOTO Command|  
|GETFLDSTATE( ) Function|GOMONTH( ) Function||  
|GETCP( ) Function|GETENV( ) Function||  
  
## H  
  
|||  
|-|-|  
|HEADER( ) Function|HOUR( ) Function|  
  
## I  
  
||||  
|-|-|-|  
|IDXCOLLATE( ) Function|IF ... ENDIF Command|IIF( ) Function|  
|INDBC( ) Function|INDEX Command|INLIST( ) Function|  
|INSERT-SQL Command|INT( ) Function|ISALPHA( ) Function|  
|ISBLANK( ) Function|ISDIGIT( ) Function|ISEXCLUSIVE( ) Function|  
|ISLEADBYTE( ) Function|ISLOWER( ) Function|ISNULL( ) Function|  
|ISREADONLY( ) Function|ISUPPER( ) Function||  
  
## K  
  
||||  
|-|-|-|  
|KEY( ) Function|KEYMATCH( ) Function||  
  
## L  
  
||||  
|-|-|-|  
|LEFT( ) Function|LEFTC( ) Function|LIKEC( ) Function|  
|LENC( ) Function|LIKE( ) Function|LOCK( ) Function|  
|LOCAL Command|LOCATE Command|LOOKUP( ) Function|  
|LOG( ) Function|LOG10( ) Function|LTRIM( ) Function|  
|LOWER( ) Function|LPARAMETERS Command||  
|LUPDATE( ) Function|LEN( ) Function||  
  
## M  
  
||||  
|-|-|-|  
|_MLINE System Memory Variable|MAX( ) Function|MDX( ) Function|  
|MDY( ) Function|MEMLINES( ) Function|MESSAGE( ) Function|  
|MIN( ) Function|MINUTE( ) Function|MLINE( ) Function|  
|MOD( ) Function|MONTH( ) Function|MTON( ) Function|  
  
## N  
  
||||  
|-|-|-|  
|NDX( ) Function|NORMALIZE( ) Function|NOT Operator|  
|NOTE Command|NTOM( ) Function|NVL( ) Function|  
  
## O  
  
||||  
|-|-|-|  
|OCCURS( ) Function|OLDVAL( ) Function|ON ERROR Command|  
|ON KEY Command|ON( ) Function|OPEN DATABASE Command|  
|OR Operator|ORDER( ) Function|OS( ) Function|  
  
## P  
  
||||  
|-|-|-|  
|PACK Command|PARAMETERS( ) Function|PAYMENT( ) Function|  
|PARAMETERS Command|PRIMARY( ) Function|PRIVATE Command|  
|PI( ) Function|PROGRAM( ) Function|PROPER( ) Function|  
|PROCEDURE Command|PV( ) Function||  
|PUBLIC Command|PADL( ) &#124; PADR( ) &#124; PADC( ) Functions||  
  
## R  
  
||||  
|-|-|-|  
|RAND( ) Function|RAT( ) Function|RATC( ) Function|  
|RATLINE( ) Function|RECALL Command|RECCOUNT( ) Function|  
|RECNO( ) Function|RECSIZE( ) Function|REGIONAL Command|  
|RELATION( ) Function|REMOVE TABLE Command|REPLACE Command|  
|REPLACE FROM ARRAY Command|REPLICATE( ) Function|RETRY Command|  
|RETURN Command|RIGHT( ) Function|RIGHTC( ) Function|  
|RLOCK( ) Function|ROLLBACK Command|ROUND( ) Function|  
|RTOD( ) Function|RTRIM( ) Function||  
  
## S  
  
||||  
|-|-|-|  
|SCAN ... ENDSCAN Command|SCATTER Command|SEC( ) Function|  
|SECONDS( ) Function|SEEK Command|SEEK( ) Function|  
|SELECT Command|SELECT( ) Function|SELECT-SQL Command|  
|SET BLOCKSIZE Command|SET CARRY Command|SET CENTURY Command|  
|SET COLLATE Command|SET DATABASE Command|SET DATE Command|  
|SET DEFAULT Command|SET DELETED Command|SET EXACT Command|  
|SET EXCLUSIVE Command|SET FDOW Command|SET FIELDS Command|  
|SET FILTER Command|SET FIXED Command|SET FULLPATH Command|  
|SET FWEEK Command|SET HOURS Command|SET INDEX Command|  
|SET LOCK Command|SET MULTILOCKS Command|SET NEAR Command|  
|SET NOCPTRANS Command|SET NOTIFY Command|SET NULL Command|  
|SET OPTIMIZE Command|SET ORDER Command|SET PATH Command|  
|SET PROCEDURE Command|SET RELATION Command|SET RELATION OFF Command|  
|SET REPROCESS Command|SET SKIP Command|SET UDFPARMS Command|  
|SET UNIQUE Command|SET VOLUME Command|SET( ) Function|  
|SETFLDSTATE( ) Function|SIGN ( ) Function|SIN( ) Function|  
|SKIP Command|SORT Command|SPACE( ) Function|  
|SQRT( ) Function|STORE Command|STR( ) Function|  
|STRCONV( ) Function|STRTRAN( ) Function|STUFF( ) Function|  
|STUFFC( ) Function|SUBSTR( ) Function|SUBSTRC( ) Function|  
|SUM Command|SYS(2011) Function||  
  
## T  
  
||||  
|-|-|-|  
|_TALLY System Memory Variable|_TRIGGERLEVEL System Memory Variable|TAGCOUNT( ) Function|  
|TABLEUPDATE( ) Function|TAG( ) Function|TARGET( ) Function|  
|TAGNO( ) Function|TAN( ) Function|TRIM( ) Function|  
|TIME( ) Function|TOTAL Command|TXNLEVEL( ) Function|  
|TTOC( ) Function|TTOD( ) Function||  
|TYPE( ) Function|TABLEREVERT( ) Function||  
  
## U  
  
||||  
|-|-|-|  
|UNIQUE( ) Function|UNLOCK Command|USE Command|  
|UPDATE Command|UPPER( ) Function||  
|USED( ) Function|UPDATE - SQL Command||  
  
## V  
  
||||  
|-|-|-|  
|VAL( ) Function|VERSION( ) Function||  
  
## W  
  
||||  
|-|-|-|  
|WEEK( ) Function|||  
  
## Y  
  
||||  
|-|-|-|  
|YEAR( ) Function|||  
  
## Z  
  
||||  
|-|-|-|  
|ZAP Command|||
