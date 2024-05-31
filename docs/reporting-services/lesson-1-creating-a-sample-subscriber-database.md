---
title: "Lesson 1: Create a sample subscriber database"
description: Learn how to create a small subscriber database to store subscription data for your data-driven subscription.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/30/2017
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---

# Lesson 1: Create a sample subscriber database

In this [!INCLUDE[ssRSnoversion_md](../includes/ssrsnoversion-md.md)] tutorial lesson, you create a small "subscriber" database to store subscription data for your data-driven subscription. When the subscription is processed, the report server retrieves this data and uses it to customize report output. For example, the rows of data include specific order numbers to use for filters and what file format generated reports are in when they're created.  
  
This lesson assumes you're using [!INCLUDE[ssManStudioFull_md](../includes/ssmanstudiofull-md.md)] to create a SQL Server database.  
  
### Create a sample subscriber database  
  
1.  Start [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], and open a connection to an instance of the [!INCLUDE[ssDEnoversion_md](../includes/ssdenoversion-md.md)].  
  
2.  Right-click on Databases, select **New Database...**.  
  
3.  In the New Database dialog box, in **Database Name**, type *Subscribers*. 

4.  Select **OK**.

5.  Select the **New Query** button on the toolbar.  
  
6.  Copy the following [!INCLUDE[tsql](../includes/tsql-md.md)] statements into the empty query:  
  
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
  
7.  Select **! Execute** on the toolbar.  
  
8.  Use a SELECT statement to verify that you have three rows of data. For example: `select * from OrderInfo`  
  
## Next step  
+ You successfully created the subscription data that drives report distribution and vary the report output for each subscriber. 
+ Next, you modify the data source properties of the report to use stored credentials. 
+ You also modify the report design to include a parameter that the subscription uses with the subscriber data. [Lesson 2: Modify the report data source properties](../reporting-services/lesson-2-modifying-the-report-data-source-properties.md).  

## Related content

- [Create a data-driven subscription](../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md)  
- [Create a database](../relational-databases/databases/create-a-database.md)  
- [Create a basic table report](../reporting-services/tutorial-step01-creating-a-report-server-project-reporting-services.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
