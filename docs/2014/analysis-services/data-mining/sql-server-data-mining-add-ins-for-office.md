---
title: "SQL Server Data Mining Add-Ins for Office | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: c9021a19-2c19-4f0a-a293-5f7e0ac2524c
author: minewiskan
ms.author: owend
manager: craigg
---
# SQL Server Data Mining Add-Ins for Office
  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Data Mining Add-ins for Office is a lightweight set of tools for predictive analytics that lets you use data in Excel to build analytical models for prediction, recommendation, or exploration.  
  
 The wizards and data management tools in the add-ins provide step-by-step instruction for these common data mining tasks:  
  
-   **Organize and clean your data prior to modeling.** Use data stored in Excel or any Excel data source. You can create and save connections to re-use data sources, repeat experiments, or re-train models.  
  
-   **Profile, sample, and prepare.** Many experienced data miners say that as much as 70-90 percent of a data mining project is spent on data preparation. The add-ins can make this task go faster, by providing visualizations in Excel and wizards that help you with these common tasks:  
  
    -   Profile data and understand its distribution and characteristics.  
  
    -   Create training and testing sets through random sampling or oversampling.  
  
    -   Find outliers and remove or replace them.  
  
    -   Re-label data to improve the quality of analysis.  
  
-   **Analyze patterns through supervised or unsupervised learning.** Click through friendly wizards to perform some of the most popular data mining tasks, including clustering analysis, market basket analysis, and forecasting.  
  
     Among the well-known machine learning algorithms included in the add-ins are Naïve Bayes, logistic regression, clustering, time series, and neural networks.  
  
     If you are new to data mining, get help building prediction queries from the **Query** wizard.  
  
     Advanced users can build custom DMX queries with the drag-and-drop **Advanced Query Editor**, or automate predictions using Excel VBA.  
  
-   **Document and manage.** After you've created a data set and built some models, document your work and your insights by generating a statistical summary of the data and model parameters.  
  
-   **Explore and visualize.** Data mining is not an activity that can be fully automated - you need to explore and understand your results to take meaningful action. The add-ins help you with exploration by providing interactive viewers in Excel, Visio templates that let you customize model diagrams, and the ability to export charts and tables to Excel for additional filtering or modification.  
  
-   **Deploy and integrate.** When you've created a useful model, put your model into production, by using the management tools to export the model from your experimental server to another instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
     You can also leave the model on the server where you created it, but refresh the training data and run predictions using Integration Services or DMX scripts.  
  
     Power users will appreciate the **Trace** functionality, that lets you see the XMLA and DMX statements sent to the server.  
  
## Getting Started  
 See these topics to learn about the tools and to get set up:  
  
-   [Data Mining Client for Excel &#40;SQL Server Data Mining Add-ins&#41;](../data-mining-client-for-excel-sql-server-data-mining-add-ins.md)  
  
-   [Table Analysis Tools for Excel](../table-analysis-tools-for-excel.md)  
  
-   [Data Mining Shapes for Visio](../data-mining-shapes-for-visio.md)  
  
-   [Connect to a Data Mining Server](../connect-to-a-data-mining-server.md)  
  
## Support and Requirements  
 The SQL Server Data Mining Add-Ins for Office is a free download. You must have one of the following versions of Office already installed to use these tools:  
  
-   Office 2010, 32-bit or 64-bit version  
  
-   Office 2013, 32-bit or 64-bit version  
  
> [!WARNING]  
>  Be sure to download the version of the add-ins that matches your version of Excel.  
  
 The Data Mining Add-ins requires a connection to one of the following editions of SQL Server Analysis Services:  
  
-   Enterprise  
  
-   Business Intelligence  
  
-   Standard  
  
 Depending on the edition of SQL Server Analysis Services that you connect to, some of the advanced algorithms might not be available. For information, see [Features Supported by the Editions of SQL Server 2014](https://msdn.microsoft.com/library/cc645993.aspx).  
  
 For additional help with installation, see this page on the Download Center: [https://www.microsoft.com/download/details.aspx?id=29061](https://www.microsoft.com/download/details.aspx?id=29061)  
  
  
