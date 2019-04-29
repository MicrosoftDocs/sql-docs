---
title: "Integration Services Features Supported by the Editions of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/26/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: e5018225-68bb-4f34-ae4a-ead79d8ad13a
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services features supported by the editions of SQL Server
 This topic provides details about the features of SQL Server Integration Services (SSIS) supported by the different editions of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)].  

For features supported by Evaluation and Developer editions, see features listed for Enterprise Edition in the following tables.
  
For the latest release notes and what's new information, see the following articles:
-   [SQL Server 2016 release notes](../sql-server/sql-server-2016-release-notes.md)
-   [What's New in Integration Services in SQL Server 2016](../integration-services/what-s-new-in-integration-services-in-sql-server-2016.md)
-   [What's New in Integration Services in SQL Server 2017](../integration-services/what-s-new-in-integration-services-in-sql-server-2017.md)
    
**Try SQL Server 2016!**    

The SQL Server Evaluation edition is available for a 180-day trial period.  
    
> [![Download from Evaluation Center](../analysis-services/media/download.png)](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016) **[Download SQL Server 2016  from the Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016)**    
    
## <a name="ISNew"></a> New Integration Services features in SQL Server 2017
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Scale Out Master|Yes|||||
|Scale Out Worker|Yes|Yes <sup>1</sup>|TBD|TBD|TBD|
|Support for Microsoft Dynamics AX and Microsoft Dynamics CRM in OData components <sup>2</sup>|Yes|Yes||||

<sup>1</sup> If you run packages that require Enterprise-only features in Scale Out, the Scale Out Workers must also run on instances of SQL Server Enterprise.

<sup>2</sup> This feature is also supported in SQL Server 2016 with Service Pack 1.

## <a name="IEWiz"></a> SQL Server Import and Export Wizard

|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|SQL Server Import and Export Wizard|Yes|Yes|Yes|Yes|Yes|  

## <a name="IS"></a> Integration Services  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Built-in data source connectors|Yes|Yes|||| 
|Built in tasks and transformations|Yes|Yes||||  
|ODBC source and destination |Yes|Yes|||| 
|Azure data source connectors and tasks|Yes|Yes||||  
|Hadoop/HDFS connectors and tasks|Yes|Yes||||  
|Basic data profiling tools|Yes|Yes|||| 

## <a name="ISAA"></a> Integration Services - Advanced sources and destinations  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|High-performance Oracle source and destination by Attunity|Yes|||||  
|High-performance Teradata source and destination by Attunity|Yes|||||  
|SAP BW source and destination|Yes|||||  
|Data mining model training destination|Yes|||||  
|Dimension processing destination|Yes|||||  
|Partition processing destination|Yes|||||  
  
## <a name="ISAT"></a> Integration Services - Advanced tasks and transformations  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|  
|Change Data Capture components by Attunity <sup>1</sup>|Yes|||||  
|Data mining query transformation|Yes|||||  
|Fuzzy grouping and fuzzy lookup transformations|Yes|||||  
|Term extraction and term lookup transformations|Yes|||||  

<sup>1</sup> The Change Data Capture components by Attunity require Enterprise edition. The Change Data Capture Service and the Change Data Capture Designer, however, do not require Enterprise edition. You can use the Designer and the Service on a computer where SSIS is not installed.
