---
title: "Install sqlcmd Command-Line Client (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a482226d-6780-439a-940f-0da171e336cf
caps.latest.revision: 8
author: BarbKess
---
# Install sqlcmd Command-Line Client (SQL Server PDW)
Describes how to install the **sqlcmd** Command-Line Client for use with SQL Server PDW. Use these instructions if you do not already have SQL Server installed and need to install the standalone version of sqlcmd.  
  
> [!NOTE]  
> If you already have SQL Server 2008 R2, SQL Server 2012, or SQL Server 2014, you already have sqlcmd and the correct version of SQL Server Native Client, unless you unchecked Client Tools during setup.  
  
Sqlcmd, when installed with SQL Server, is located in these directories:  
  
-   SQL Server 2014: Client SDK\ODBC\110\Tools\Binn  
  
-   SQL Server 2012: Client SDK\ODBC\110\Tools\Binn  
  
-   SQL Server 2008 R2: Client SDK\ODBC\100\Tools\Binn  
  
## Contents  
  
-   [Before You Begin](#before)  
  
-   [Install sqlcmd](#install)  
  
-   [Next Steps](#next)  
  
## <a name="before"></a>Before You Begin  
To install SQL Server Native Client, see [Install SQL Server Native Client &#40;SQL Server PDW&#41;](../sqlpdw/install-sql-server-native-client-sql-server-pdw.md).  
  
### Software Prerequisites - SQL Server 2014  
SQL Server Native Client 11.0  
  
SQL Server 2014 ships with the SQL Server 2012 version of sqlcmd.  
  
### Software Prerequisites - SQL Server 2012  
  
-   SQL Server Native Client 11.0  
  
### Software Prerequisites - SQL Server 2008 R2  
  
-   SQL Server Native Client 10.0 (release 10.5)  
  
## <a name="install"></a>Install sqlcmd  
To install sqlcmd as a standalone package, use the following download links. These links are also listed on the [Microsoft SQL Server 2012 Feature Pack](http://www.microsoft.com/en-us/download/details.aspx?id=29065) page on MSDN.  
  
-   [X86 Package (SqlCmdLnUtils.msi)](http://go.microsoft.com/fwlink/?LinkID=239649&clcid=0x409)  
  
-   [X64 Package (SqlCmdLnUtils.msi)](http://go.microsoft.com/fwlink/?LinkID=239650&clcid=0x409)  
  
## <a name="next"></a>Next Steps  
To run sqlcmd with SQL Server Native Client 11.0, use the –I option.  
  
For more information on using sqlcmd with SQL Server PDW, see [Connect With sqlcmd Command-Line SQL Client &#40;SQL Server PDW&#41;](../sqlpdw/connect-with-sqlcmd-command-line-sql-client-sql-server-pdw.md).  
  
For the sqlcmd reference for SQL Server 2012, see [sqlcmd Utility](http://msdn.microsoft.com/en-US/library/ms162773(v=sql.110).aspx).  
  
## See Also  
[Client Tools and Applications &#40;SQL Server PDW&#41;](../sqlpdw/client-tools-and-applications-sql-server-pdw.md)  
  
