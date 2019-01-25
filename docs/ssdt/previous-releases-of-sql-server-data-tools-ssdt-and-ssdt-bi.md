---
title: "Previous releases of SQL Server Data Tools (SSDT and SSDT-BI) | Microsoft Docs"
ms.custom: ""
ms.date: "09/05/2018"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssdt
ms.topic: conceptual
ms.assetid: 5d32e301-0f44-4916-b0db-76e8322c0ab7
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||=azuresqldb-mi-current"
---
# Previous releases of SQL Server Data Tools (SSDT and SSDT-BI)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
SQL Server Data Tools (SSDT) provides project templates and design surfaces for building SQL Server content types - relational databases, Analysis Services models, Reporting Services reports, and Integration Services packages.  
  
SSDT is backwards compatible, so you can always use [the newest SSDT](download-sql-server-data-tools-ssdt.md) to design and deploy databases, models, reports, and packages that run on older versions of SQL Server.  
  
> [!NOTE]  
> Historically, the Visual Studio shell used to create SQL Server content types has been released under various names, including **SQL Server Data Tools**, **SQL Server Data Tools - Business Intelligence**, and **Business Intelligence Development Studio**. Previous versions came with distinct sets of project templates. To get all of the project templates together in one SSDT, you need [the newest version](download-sql-server-data-tools-ssdt.md). Otherwise, you will probably need to install multiple previous versions to get all of the templates used in SQL Server.  Only one shell is installed per version of Visual Studio; installing a second SSDT just adds the missing templates.  

## Recent downloads

The last few downloads are provided for the unlikely event that you experience issues with the latest release.

|SSDT release| Visual Studio 2017|
|:---|:---|
|15.8.0|[SSDT for VS2017 15.8.0](https://go.microsoft.com/fwlink/?linkid=2014060)|
|15.7.1|[SSDT for VS2017 15.7.1](https://go.microsoft.com/fwlink/?LinkId=875613)|
|15.7.0|[SSDT for VS2017 15.7.0](https://go.microsoft.com/fwlink/?LinkId=874716)|
|15.6.0|[SSDT for VS2017 15.6.0](https://go.microsoft.com/fwlink/?LinkId=871368)|

<br>

|SSDT release| Visual Studio 2015|
|:---|:---|
|17.4|[SSDT for VS2015 17.4](https://go.microsoft.com/fwlink/?linkid=863440)|
|17.3|[SSDT for VS2015 17.3](https://go.microsoft.com/fwlink/?linkid=858660)|
|16.5|[SSDT for VS2015 16.5](https://go.microsoft.com/fwlink/?LinkID=832313)|  

<br>

|SSDT release| Visual Studio 2013|
|:---|:---|
|16.5|[SSDT for VS2013 16.5](https://go.microsoft.com/fwlink/?LinkID=832308)|  

<br>


\* SSDT supports the two most recent versions of Visual Studio. With the release of Visual Studio 2017, SSDT for VS2013 is no longer being updated. For additional information, see the *FAQ* section of [this SSDT team blog post](https://blogs.msdn.microsoft.com/ssdt/2017/03/10/sql-server-data-tools-17-0-rc-and-ssdt-in-vs2017/).

  
## Links to Download pages 
**SQL Relational: Database Engine**  
  
Provides templates for building relational databases for the RDBMS and Azure SQL Database. SSDT is version agnostic with respect to relational database design. You can use either the Visual Studio 2012 or 2013 version with any version of SQL Server Database Engine or Azure SQL Database.  
  
**Database Designers**  
  
-   [Download SSDT for Visual Studio 2013](https://msdn.microsoft.com/dn864412)  
  
-   [Download SSDT for Visual Studio 2012](https://msdn.microsoft.com/jj650015)  
  
-   **SSDT for Visual Studio 2010** is no longer available so please choose a newer version. Newer versions of SSDT run side-by-side with existing Visual Studio 2010 installations. It's not necessary to have SSDT match the full-product version of Visual Studio on your computer.  
  
Visual Studio 2013 customers can download a preview version of SSDT to try out new features that are not yet in the product release version.  
  
**SQL BI: Analysis Services, Reporting Services, Integration services**  
  
BI templates are used to create SSAS models, SSRS reports, and SSIS packages. BI Designers are tied to specific releases of SQL Server. To use the newer BI features, install a newer versioned BI designer.  
  
**BI Designers**  
  
[Download SSDT-BI for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=42313) (SQL Server 2014, SQL Server 2012, SQL Server 2008, and 2008 R2)  
  
[Download SSDT-BI for Visual Studio 2012](https://www.microsoft.com/download/details.aspx?id=36843) (SQL Server 2014, SQL Server 2012, SQL Server 2008, and 2008 R2  
  
Business Intelligence Development Studio (BIDS) is installed via SQL Server Setup. There is no web download. (SQL Server 2008, and 2008 R2)  
  
For SQL Server 2012 or 2014, you can use either **SSDT-BI for Visual Studio 2012** or **SSDT-BI for Visual Studio 2013**. The only difference between the two is the Visual Studio version.  
  
## See Also  
[Download SQL Server Data Tools &#40;SSDT&#41;](../ssdt/download-sql-server-data-tools-ssdt.md)  
[Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md)  
[SQL Tools and Utilities](../tools/overview-sql-tools.md)
