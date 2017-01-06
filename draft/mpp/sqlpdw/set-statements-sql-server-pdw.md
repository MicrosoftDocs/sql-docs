---
title: "SET Statements (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 94bedb64-1b8b-4517-9c27-de8e258fcbd9
caps.latest.revision: 14
author: BarbKess
---
# SET Statements (SQL Server PDW)
SQL Server PDW supports several SET statements that affect the way the current session handles specific information. There is no requirement to use the SET statements; they are provided for compatibility with client tools and existing scripts.  
  
## SET Statements  
The SET statements are based on the SQL Server SET statements. However, most SET statements in SQL Server PDW have only one valid value, which is already set. Therefore, there is no requirement to use the SET statements. As mentioned in the introduction, they are provided for compatibility with client tools and existing scripts.  
  
This SQL Server PDW documentation contains the SET statements supported by SQL Server PDW. For a full explanation of each SET statement, see the SQL Server documentation. The documentation links and valid values are described in the following table.  
  
|Supported Setting|Valid Value|SQL Server PDW Documentation|SQL Server Documentation|  
|---------------------|---------------|-------------------------------------------------------|---------------------------------------------------------------------|  
|SET ANSI_DEFAULTS|ON|[SET ANSI_DEFAULTS &#40;SQL Server PDW&#41;](../sqlpdw/set-ansi-defaults-sql-server-pdw.md)|[SET ANSI_DEFAULTS (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188340(v=sql11))|  
|SET ANSI_NULL_DFLT_OFF|OFF|[SET ANSI_NULL_DFLT_OFF &#40;SQL Server PDW&#41;](../sqlpdw/set-ansi-null-dflt-off-sql-server-pdw.md)|[SET ANSI_NULL_DFLT_OFF (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187356(v=sql11))|  
|SET ANSI_NULL_DFLT_ON|ON|[SET ANSI_NULL_DFLT_ON &#40;SQL Server PDW&#41;](../sqlpdw/set-ansi-null-dflt-on-sql-server-pdw.md)|[SET ANSI_NULL_DFLT_ON (Transact-SQL](http://msdn.microsoft.com/en-us/library/ms187375(v=sql11))|  
|SET ANSI_NULLS|ON|[SET ANSI_NULLS &#40;SQL Server PDW&#41;](../sqlpdw/set-ansi_nulls-sql-server-pdw.md)|[SET ANSI_NULLS (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188048(v=sql11))|  
|SET ANSI_PADDING|ON|[SET ANSI_PADDING &#40;SQL Server PDW&#41;](../sqlpdw/set-ansi-padding-sql-server-pdw.md)|[SET ANSI_PADDING (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187403(v=sql11))|  
|SET ANSI_WARNINGS|ON|[SET ANSI_WARNINGS &#40;SQL Server PDW&#41;](../sqlpdw/set-ansi-warnings-sql-server-pdw.md)|[SET ANSI_WARNINGS (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190368(v=sql11))|  
|SET ARITHABORT|ON|[SET ARITHABORT &#40;SQL Server PDW&#41;](../sqlpdw/set-arithabort-sql-server-pdw.md)|[SET ARITHABORT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190306(v=sql11))|  
|SET ARITHIGNORE|OFF|[SET ARITHIGNORE &#40;SQL Server PDW&#41;](../sqlpdw/set-arithignore-sql-server-pdw.md)|[SET ARITHIGNORE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms184341(v=sql11))|  
|SET CONCAT_NULL_YIELDS_NULL|ON|[SET CONCAT_NULL_YIELDS_NULL  &#40;SQL Server PDW&#41;](../sqlpdw/set-concat-null-yields-null-sql-server-pdw.md)|[SET CONCAT_NULL_YIELDS_NULL (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms176056(v=sql11).aspx)|  
|SET DATEFIRST|7|[SET DATEFIRST &#40;SQL Server PDW&#41;](../sqlpdw/set-datefirst-sql-server-pdw.md)|[SET DATEFIRST (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms181598(v=sql11).aspx)|  
|SET DATEFORMAT|mdy|[SET DATEFORMAT &#40;SQL Server PDW&#41;](../sqlpdw/set-dateformat-sql-server-pdw.md)|[SET DATEFIRST (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms181598(v=sql11).aspx)|  
|SET FMTONLY|ON &#124; OFF|[SET FMTONLY &#40;SQL Server PDW&#41;](../sqlpdw/set-fmtonly-sql-server-pdw.md)|[SET FMTONLY (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms173839(v=sql11).aspx)|  
|SET IMPLICIT_TRANSACTIONS|ON &#124; OFF|[SET IMPLICIT_TRANSACTIONS &#40;SQL Server PDW&#41;](../sqlpdw/set-implicit-transactions-sql-server-pdw.md)|[SET IMPLICIT_TRANSACTIONS Transact-SQL](http://msdn.microsoft.com/en-us/library/ms187807(v=sql11))|  
|SET LOCK_TIMEOUT|-1<br /><br />Values > -1 are valid, but ignored.|[SET LOCK_TIMEOUT &#40;SQL Server PDW&#41;](../sqlpdw/set-lock-timeout-sql-server-pdw.md)|[SET LOCK_TIMEOUT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms189470(v=sql11).aspx)|  
|SET NUMERIC_ROUNDABORT|ON|[SET NUMERIC_ROUNDABORT &#40;SQL Server PDW&#41;](../sqlpdw/set-numeric-roundabort-sql-server-pdw.md)|[SET NUMERIC_ROUNDABORT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188791(v=sql11))|  
|SET QUOTED_IDENTIFIER|ON|[SET QUOTED_IDENTIFIER &#40;SQL Server PDW&#41;](../sqlpdw/set-quoted_identifier-sql-server-pdw.md)|[SET QUOTED_IDENTIFIER (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms174393(v=sql11))|  
|SET ROWCOUNT|*n*|[SET ROWCOUNT &#40;SQL Server PDW&#41;](../sqlpdw/set-rowcount-sql-server-pdw.md)|[SET ROWCOUNT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188774.aspx)|  
|SET TEXTSIZE|Max is 2147483648.|[SET TEXTSIZE &#40;SQL Server PDW&#41;](../sqlpdw/set-textsize-sql-server-pdw.md)|[SET TEXTSIZE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms186238(v=sql11).aspx)|  
|SET TRANSACTION ISOLATION LEVEL|READ UNCOMMITTED|[SET TRANSACTION ISOLATION LEVEL &#40;SQL Server PDW&#41;](../sqlpdw/set-transaction-isolation-level-sql-server-pdw.md)|[SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms173763(v=sql11))|  
|SET XACT_ABORT|ON|[SET XACT_ABORT &#40;SQL Server PDW&#41;](../sqlpdw/set-xact-abort-sql-server-pdw.md)|[SET XACT_ABORT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188792(v=sql11).aspx)|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
