---
title: "Enterprise Information Management using SSIS, MDS, and DQS Together [Tutorial] | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "data-quality-services"
  - "integration-services"
  - "master-data-services"
ms.topic: conceptual
ms.assetid: ba09b504-3007-4cb7-8ef8-f01adbf51646
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Enterprise Information Management using SSIS, MDS, and DQS Together [Tutorial]
  Managing information in an enterprise typically involves integrating data from across the enterprise and beyond, cleansing the data, matching the data to remove any duplicates, standardizing the data, enriching the data, making the data conform to legal and compliance requirements, and then storing the data in a centralized location with all the necessary security settings.  
  
 [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] provides all the components needed for an effective Enterprise Information Management (EIM) solution in a single product. Key components that help you build an EIM solution are:  
  
-   SQL Server Integration Services  
  
-   SQL Server Data Quality Services  
  
-   SQL Server Master Data Services  
  
 SQL Server Integration Services (SSIS) provides a powerful, extensible platform for integrating data from various sources in a comprehensive extract, transform, and load (ETL) solution that supports business workflows, a data warehouse, or master data management. See [Integration Services Overview](https://msdn.microsoft.com/library/ms141263\(SQL.105\).aspx) topic for a quick overview and typical uses of SSIS.  
  
 SQL Server Data Quality Services (DQS) enables you to cleanse, match, standardize, and enrich data, so you can deliver trusted information for business intelligence, a data warehouse, and transaction processing workloads. See [Introducing Data Quality Services](https://msdn.microsoft.com/library/ff877917.aspx) topic for the business need for DQS and how DQS answers the need.  
  
 SQL Server Master Data Services (MDS) provides a central data hub that ensures that the integrity of information and consistency of data is constant across different applications. See [Master Data Services Overview](../master-data-services/master-data-services-overview-mds.md) topic for brief descriptions of important features of MDS.  
  
 See [Cleansing and Matching Master Data using EIM Technologies](https://msdn.microsoft.com/library/hh403491.aspx) whitepapers for a comprehensive guidance on implementing an EIM solution using these Microsoft EIM technologies together and watch [Enterprise Information Management (EIM): Bringing together SSIS, DQS, and MDS](https://go.microsoft.com/fwlink/?LinkId=258672) video for a cool demonstration of an EIM scenario.  
  
 In this tutorial, you learn how to use SSIS, MDS, and DQS together to implement a sample Enterprise Information Management (EIM) solution. First, you use DQS to create a knowledgebase that contains knowledge about the data (metadata), cleanse the data in an Excel file by using the knowledge base, and match the data to identify and remove duplicates in the data. Next, you use the MDS Add-in for Excel to upload the cleansed and matched data to MDS. Then, you automate the whole process by using an SSIS solution. The SSIS solution in this tutorial reads the input data from an Excel file, but you can extend it to read from various sources such as Oracle, Teradata, DB2, and Windows Azure SQL Database.  
  
## Prerequisites  
  
1.  Microsoft SQL Server 2012 with the following components installed.  
  
    1.  Integration Services (SSIS)  
  
    2.  Master Data Services (MDS)  
  
    3.  Data Quality Services (DQS)  
  
    4.  SQL Server Data Tools  
  
         See [SQL Server 2012 Installation Guide](../database-engine/install-windows/installation-for-sql-server.md) for details about installing the product.  
  
2.  [Configure MDS using Master Data Services Configuration Manager](https://msdn.microsoft.com/library/ee633884.aspx)  
  
     Use the Configuration Manager to create and configure a Master Data Services database. After you create the MDS database, create a web application for MDS in a web site (for example: [http://localhost/MDS](http://localhost/MDS)) and associate the MDS database with the MDS web application. Note that, to create an MDS web application, you should have IIS installed on your computer. See [Web Application Requirements (Master Data Services)](https://msdn.microsoft.com/library/ee633744.aspx) and [Database Requirements (Master Data Services)](https://msdn.microsoft.com/library/ee633767.aspx) for details about the prerequisites for configuring MDS database and web application.  
  
3.  [Install and Configure DQS using Data Quality Server Installer](https://msdn.microsoft.com/library/hh231682.aspx). Click **Start**, click **All Programs**, click **Microsoft SQL Server 2014**, click **Data Quality Services**, and then click **Data Quality Server Installer**.  
  
4.  Microsoft Excel 2010 (32-bit is preferred).  
  
5.  Install **Master Data Services Add-in for Excel** (32-bit or 64-bit based on the version of Excel you have on your computer) from [here](https://www.microsoft.com/download/details.aspx?id=29064). To find the version of Excel installed on your computer, run **Excel**, click **File** on menu bar and click **Help** to see the version in the right pane. Note that you need to install Visual Studio 2010 Tools for Office Runtime before installing the Excel Add-in.  
  
6.  (Optional) Create an account with [Windows Azure Marketplace](https://datamarket.azure.com/). One of the tasks in the tutorial requires you to have an **Azure Marketplace** (originally named **Data Market**) account. You can skip this task if you want and proceed with the next task.  
  
7.  Download the Suppliers.xls file from [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkId=271504).  
  
8.  DQS does not allow you to export the cleansing or matching results to an Excel file if you are using **64-bit version of Excel**. This issue is a known issue. To work around the issue, do the following:  
  
    1.  Run **DQLInstaller.exe -upgrade**. If you installed the default instance of SQL Server, the DQSInstaller.exe file is available at C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Binn. Double-click the DQSInstaller.exe file.  
  
    2.  In **Master Data Services Configuration Manager**, click **Select Database**, select existing **MDS** database, and then click **Upgrade**.  
  
## Lessons  
  
|Lesson|Brief description|Estimated time to complete (in minutes).|  
|------------|-----------------------|------------------------------------------------|  
|[Lesson 1: Creating the Suppliers DQS Knowledge Base](../../2014/tutorials/lesson-1-creating-the-suppliers-dqs-knowledge-base.md)|In this lesson, you create a DQS knowledge base named **Suppliers**.|60|  
|[Lesson 2: Cleansing Supplier Data using the Suppliers Knowledge Base](../../2014/tutorials/lesson-2-cleansing-supplier-data-using-the-suppliers-knowledge-base.md)|In this lesson, you create and run a DQS project to cleanse the supplier data in an Excel file by using the **Suppliers** Knowledge Base you created in the first lesson.|45|  
|[Lesson 3: Matching Data to Remove Duplicates from Supplier List](../../2014/tutorials/lesson-3-matching-data-to-remove-duplicates-from-supplier-list.md)|In this lesson, you create a DQS project to perform matching activity to identify and remove duplicates from the cleansed suppler list.|45|  
|[Lesson 4: Storing Supplier Data in MDS](../../2014/tutorials/lesson-4-storing-supplier-data-in-mds.md)|In this lesson, you upload the cleansed and matched supplier data to Master Data Services (MDS) by using the **MDS Add-in for Excel**.|45|  
|[Lesson 5: Automating the Cleansing and Matching using SSIS](../../2014/tutorials/lesson-5-automating-the-cleansing-and-matching-using-ssis.md)|In this lesson, you create an SSIS solution that cleanses input data by using DQS, matches the cleansed data to remove duplicates, and stores the cleansed and matched data on MDS in an automated manner.|75|  
  
## Next Steps  
 To begin the tutorial, continue to the first lesson: [Lesson 1: Creating the Suppliers DQS Knowledge Base](../../2014/tutorials/lesson-1-creating-the-suppliers-dqs-knowledge-base.md).  
  
  
