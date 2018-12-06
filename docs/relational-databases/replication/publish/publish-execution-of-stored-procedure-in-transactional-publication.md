---
title: "Publish Execution of Stored Procedure in Transactional Publication | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publishing [SQL Server replication], stored procedure execution"
  - "stored procedures [SQL Server replication], publishing"
ms.assetid: 1d3a3525-0bc5-466f-b097-5359dc74432d
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Publish Execution of Stored Procedure in Transactional Publication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Specify that the execution of a stored procedure (rather than just its definition) should be published in the **Article Properties - \<Article>** dialog box. This dialog box is available in the New Publication Wizard and the **Publication Properties - \<Publication>** dialog box. For more information about using the wizard and accessing the dialog box, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md).  
  
 The definition of the procedure (the CREATE PROCEDURE statement) is replicated to the Subscriber when the subscription is initialized; when the procedure is executed at the Publisher, replication executes the corresponding procedure at the Subscriber.  
  
### To publish the execution of a stored procedure  
  
1.  On the **Articles** page of the New Publication Wizard or the **Publication Properties - \<Publication>** dialog box, select a stored procedure.  
  
2.  Click **Article Properties**, and then click **Set Properties of Highlighted Stored Procedure**.  
  
3.  In the **Article Properties - \<Article>** dialog box, specify one of the following values for the **Replicate** option:  
  
    -   **Execution of the stored procedure**  
  
    -   **Execution in a serialized transaction of the SP**  
  
         This is the recommended option, because it replicates the procedure execution only if the procedure is executed within the context of a serializable transaction. If the stored procedure is executed outside of a serializable transaction, changes to data in published tables are replicated as a series of data manipulation language (DML) statements.  
  
4.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
5.  If you are in the **Publication Properties - \<Publication>** dialog box, click **OK** to save and close the dialog box.  
  
## See Also  
 [Publishing Stored Procedure Execution in Transactional Replication](../../../relational-databases/replication/transactional/publishing-stored-procedure-execution-in-transactional-replication.md)  
  
  
