---
title: "HOST_NAME Values | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql12.rep.newsubwizard.hostnamevalue.f1"
ms.assetid: 21548f08-2910-4a55-baac-b911ba9afaf1
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# HOST_NAME Values
  Merge publications with parameterized filters use the SUSER_SNAME() function and/or the HOST_NAME() function to filter data. The function is specified in the New Publication Wizard or the **Publication Properties** dialog box.  
  
 By default, the HOST_NAME() function returns the name of the computer connecting to the Publisher. When using parameterized filters, it is common to override this value by supplying a value on this page of the wizard. The HOST_NAME() function then returns the value you specify rather than the name of the computer. For more information, see the "Overriding the HOST_NAME() Value" section of [Parameterized Row Filters](merge/parameterized-filters-parameterized-row-filters.md).  
  
> [!NOTE]  
>  If you override HOST_NAME(), all calls to the HOST_NAME() function will return the value you specify. Ensure that other applications are not depending on HOST_NAME() returning the computer name.  
  
## Options  
 **Subscription properties**  
 Enter a value for each Subscriber in the **HOST_NAME Value** column or accept the default, which is the name of the Subscriber computer.  
  
## See Also  
 [Create a Pull Subscription](create-a-pull-subscription.md)   
 [Create a Push Subscription](create-a-push-subscription.md)   
 [View and Modify Pull Subscription Properties](view-and-modify-pull-subscription-properties.md)   
 [View and Modify Push Subscription Properties](view-and-modify-push-subscription-properties.md)   
 [HOST_NAME &#40;Transact-SQL&#41;](/sql/t-sql/functions/host-name-transact-sql)   
 [Subscribe to Publications](subscribe-to-publications.md)  
  
  
