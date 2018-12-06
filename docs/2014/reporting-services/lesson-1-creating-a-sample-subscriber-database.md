---
title: "Lesson 1: Creating a Sample Subscriber Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 47a882b7-efe5-4ee6-bef4-06118eb56903
author: markingmyname
ms.author: maghan
manager: craigg
---
# Lesson 1: Creating a Sample Subscriber Database
  Before you can define a data-driven subscription, you must have a data source that provides subscription data. In this step, you will create a small database to store the subscription data used in this tutorial. Later, when the subscription is processed, the report server retrieves this data and uses it to customize report output, delivery options, and report presentation format.  
  
 This lesson assumes you are using [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] to create a [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] database.  
  
### To create a sample Subscriber database  
  
1.  Start [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], and open a connection to a [!INCLUDE[ssDE](../includes/ssde-md.md)].  
  
2.  Right-click on Databases, select **New Database...**.  
  
3.  In the New Database dialog box, in Database Name, type *Subscribers*. [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
4.  Click the **New Query** button on the toolbar.  
  
5.  Copy the following [!INCLUDE[tsql](../includes/tsql-md.md)] statements into the empty query:  
  
    ```  
    Use Subscribers  
    CREATE TABLE [dbo].[OrderInfo] (  
        [SubscriptionID] [int] NOT NULL PRIMARY KEY ,  
        [Order] [nvarchar] (20) NOT NULL,  
        [FileType] [bit],  
        [Format] [nvarchar] (20) NOT NULL ,  
    ) ON [PRIMARY]  
    GO  
  
    INSERT INTO [dbo].[OrderInfo] (SubscriptionID, [Order], FileType, Format)   
    VALUES ('1', 'so43659', '1', 'IMAGE')  
    INSERT INTO [dbo].[OrderInfo] (SubscriptionID, [Order], FileType, Format)   
    VALUES ('2', 'so43664', '1', 'MHTML')  
    INSERT INTO [dbo].[OrderInfo] (SubscriptionID, [Order], FileType, Format)   
    VALUES ('3', 'so43668', '1', 'PDF')  
    INSERT INTO [dbo].[OrderInfo] (SubscriptionID, [Order], FileType, Format)   
    VALUES ('4', 'so71949', '1', 'Excel')  
    GO  
    ```  
  
6.  Click **! Execute** on the toolbar.  
  
7.  Use a SELECT statement to verify that you have three rows of data. For example: `select * from OrderInfo`  
  
## Next Steps  
 You have successfully created the subscription data that will drive report distribution and vary the report output for each subscriber. Next, you will modify the data source properties of the report you will distribute to subscribers. Modifying the data source properties is done to prepare the report for data-driven subscription delivery. You will also modify the report design to include a parameter that the subscription will use with the subscriber data. [Lesson 2: Modifying the Report Data Source Properties](lesson-2-modifying-the-report-data-source-properties.md).  
  
## See Also  
 [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](create-a-data-driven-subscription-ssrs-tutorial.md)   
 [Create a Database](../relational-databases/databases/create-a-database.md)   
 [Create a Basic Table Report &#40;SSRS Tutorial&#41;](create-a-basic-table-report-ssrs-tutorial.md)  
  
  
