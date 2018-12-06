---
title: "Lesson 2. Create a policy on container and generate a Shared Access Signature (SAS) key | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 41674d9d-8132-4bff-be4d-85a861419f3d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Lesson 2. Create a policy on container and generate a Shared Access Signature (SAS) key
  In this lesson, you will learn how to create a policy on the Blob container and also generate a SAS key.  
  
 A stored access policy provides an additional level of control over shared access signatures on the server side. A shared access signature is a URI that grants restricted access rights to containers, blobs, queues, and tables. When using this new enhancement, you need to create a policy on a container with read, write, and list rights.  
  
 You can create a policy and a shared access signature by using one of the following methods:  
  
-   Windows Azure REST API operations: [Create Container](https://msdn.microsoft.com/library/azure/dd179468.aspx), [Set Container ACL](https://msdn.microsoft.com/library/azure/dd179391.aspx), and [Get Container ACL](https://msdn.microsoft.com/library/azure/dd179469.aspx).  
  
-   [CloudBlobContainer.GetSharedAccessSignature Method](https://msdn.microsoft.com/library/azure/microsoft.windowsazure.storageclient.cloudblobcontainer.getsharedaccesssignature.aspx) in the Windows Azure SDK.  
  
    ```  
  
       string signature = blob.GetSharedAccessSignature(new SharedAccessPolicy()   
        {   
            // Specify the expiration time for the signature.   
            SharedAccessExpiryTime = DateTime.Now.Years(1),   
            // Specify the permissions granted by the signature.    
            Permissions = SharedAccessPermissions.Write | SharedAccessPermissions.Read   
        });  
  
    ```  
  
-   A third-party Windows Azure explorer tool, such as [Azure Storage Explorer](http://azurestorageexplorer.codeplex.com/).  
  
 **Next Lesson:**  
  
 [Lesson 3: Create a SQL Server Credential](../relational-databases/lesson-2-create-a-sql-server-credential-using-a-shared-access-signature.md)  
  
  
