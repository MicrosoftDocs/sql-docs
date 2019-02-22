---
title: "SQL Server data files in Microsoft Azure | Microsoft Docs"
ms.custom: ""
ms.date: "10/02/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
ms.assetid: 38ffd9c2-18a5-43d2-b674-e425addec4e4
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server data files in Microsoft Azure
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  ![Data files on Azure](../../relational-databases/databases/media/data-files-on-azure.png "Data files on Azure")  
  
 SQL Server Data Files in Microsoft Azure enables native support for SQL Server database files stored as Microsoft Azure Blobs. It allows you to create a database in SQL Server running in on-premises or in a virtual machine in Microsoft Azure with a dedicated storage location for your data in Microsoft Azure Blob Storage. This enhancement especially simplifies to move databases between machines by using detach and attach operations. In addition, it provides an alternative storage location for your database backup files by allowing you to restore from or to Microsoft Azure Storage. Therefore, it enables several hybrid solutions by providing several benefits for data virtualization, data movement, security and availability, and any easy low costs and maintenance for high-availability and elastic scaling.
 
> [!IMPORTANT]  
>  Storing system databases in Azure blob storage is not recommended and is not supported. 

  
 This topic introduces concepts and considerations that are central to storing SQL Server data files in Microsoft Azure Storage Service.  
  
 For a practical hands-on experience on how to use this new feature, see [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases ](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md).  
  
## Why use SQL Server data files in Microsoft Azure? 
  
-   **Easy and fast migration benefits:** This feature simplifies the migration process by moving one database at a time between machines in on-premises as well as between on-premises and cloud environments without any application changes. Therefore, it supports an incremental migration while maintaining your existing on-premises infrastructure in place. In addition, having access to a centralized data storage simplifies the application logic when an application needs to run in multiple locations in an on-premises environment. In some cases, you may need to rapidly setup computer centers in geographically dispersed locations, which gather data from many different sources. By using this new enhancement, instead of moving data from one location to another, you can store many databases as Microsoft Azure blobs, and then run Transact-SQL scripts to create databases on the local machines or virtual machines.  
  
-   **Cost and limitless storage benefits:** This feature enables you to have limitless off-site storage in Microsoft Azure while leveraging on-premises compute resources. When you use Microsoft Azure as a storage location, you can easily focus on the application logic without the overhead of hardware management. If you lose a computation node on-premises, you can set up a new one without any data movement.  
  
-   **High availability and disaster recovery benefits:** Using SQL Server Data Files in Microsoft Azure feature might simplify the high availability and disaster recovery solutions. For example, if a virtual machine in Microsoft Azure or an instance of SQL Server crashes, you can re-create your databases in a new SQL Server instance by just re-establishing links to Microsoft Azure Blobs.  
  
-   **Security benefits:** This new enhancement allows you to separate a compute instance from a storage instance. You can have a fully encrypted database with decryption only occurring on compute instance but not in a storage instance. In other words, using this new enhancement, you can encrypt all data in public cloud using Transparent Data Encryption (TDE)  certificates, which are physically separated from the data. The TDE keys can be stored in the master database, which is stored locally in your physically secure on-premises computer and backed up locally. You can use these local keys to encrypt the data, which resides in Microsoft Azure Storage. If your cloud storage account credentials are stolen, your data still stays secure as the TDE certificates always reside in on-premises.  
  
-   **Snapshot backup:**  This feature enables you to use Azure snapshots to provide nearly instantaneous backups and quicker restores for database files stored using the Azure Blob storage service. This capability enables you to simplify your backup and restore policies. For more information, see [File-Snapshot Backups for Database Files in Azure](../../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).  
  
## Concepts and Requirements  
  
### Azure Storage Concepts  
 When using SQL Server Data Files in Windows Azure feature, you need to create a storage account and a container in Windows Azure. Then, you need to create a SQL Server credential, which includes information on the policy of the container as well as a shared access signature that is necessary to access the container.  
  
 In [Microsoft Azure](https://azure.microsoft.com), an [Azure storage](https://azure.microsoft.com/services/storage/) account represents the highest level of the namespace for accessing Blobs. A storage account can contain an unlimited number of containers, as long as their total size is below the storage limits. For the latest information on storage limits, see [Azure Subscription and Service Limits, Quotas, and Constraints](https://docs.microsoft.com/azure/azure-subscription-service-limits). A container provides a grouping of a set of [Blobs](https://docs.microsoft.com/azure/storage/common/storage-introduction#blob-storage). All Blobs must be in a container. An account can contain an unlimited number of containers. Similarly, a container can store an unlimited number of Blobs as well. There are two types of blobs that can be stored in Azure Storage: block and page blobs. This new feature uses Page blobs, which are more efficient when ranges of bytes in a file are modified frequently. You can access Blobs using the following URL format: `https://storageaccount.blob.core.windows.net/<container>/<blob>`.  
  
### Azure billing considerations  
 Estimating the cost of using Azure Services is an important matter in the decision making and planning process. When storing SQL Server data files in Azure Storage, you need to pay costs associated with storage and transactions. In addition, the implementation of SQL Server Data Files in Azure Storage feature requires a renewal of Blob lease every 45 to 60 seconds implicitly. This also results in transaction costs per database file, such as .mdf or .ldf. Use the information on the [Azure Pricing](https://azure.microsoft.com/pricing/) page to help estimate the monthly costs associated with the use of Azure Storage and Azure Virtual Machines.  
  
### SQL Server concepts  
 When using this new enhancement, you are required to do the followings:  
  
-   You must create a policy on a container and also generate a shared access signature (SAS) key.  
  
-   For each container used by a data or a log file, you must create a SQL Server Credential whose name matches the container path.  
  
-   You must store the information regarding Azure Storage container, its associated policy name, and SAS key in the SQL Server credential store.  
  
 The following example assumes that an Azure storage container has been created, and a policy has been created with read, write, list, rights. Creating a policy on a container generates a SAS key which is safe to keep unencrypted in memory and needed by SQL Server to access the blob files in the container. In the following code snippet, replace `'<your SAS key>'` with an entry similar to the following: `'sr=c&si=<MYPOLICYNAME>&sig=<THESHAREDACCESSSIGNATURE>'`. For more information, see [Manage Access to Azure Storage Resources](https://docs.microsoft.com/azure/storage/blobs/storage-manage-access-to-resources)  
  
```sql
CREATE CREDENTIAL [https://testdb.blob.core.windows.net/data]  
WITH IDENTITY='SHARED ACCESS SIGNATURE',  
SECRET = '<your SAS key>'  
  
CREATE DATABASE testdb   
ON  
( NAME = testdb_dat,  
    FILENAME = 'https://testdb.blob.core.windows.net/data/TestData.mdf' )  
 LOG ON  
( NAME = testdb_log,  
    FILENAME =  'https://testdb.blob.core.windows.net/data/TestLog.ldf')  
```  
  
 **Important note:** If there are any active references to data files in a container, attempts to delete the corresponding SQL Server credential fails.  
  
### Security  
 The following are security considerations and requirements when storing SQL Server Data Files in Azure Storage.  
  
-   When creating a container for the Azure Blob storage service, we recommend that you set the access to private. When you set the access to private, container and blob data can be read by the Azure account owner only.  
  
-   When storing SQL Server database files in Azure Storage, you need to use a shared access signature, a URI that grants restricted access rights to containers, blobs, queues, and tables. By using a shared access signature, you can enable SQL Server to access resources in your storage account without sharing your Azure storage account key.  
  
-   In addition, we recommend that you continue implementing the traditional on-premises security practices for your databases.  
  
### Installation prerequisites  
 The followings are installation prerequisites when storing SQL Server Data Files in Azure.  
  
-   **SQL Server on-premises:** SQL Server 2016 and later include this feature. To learn how to download the latest version of SQL Server, see [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads).  
  
-   SQL Server running in an Azure virtual machine: If you are installing [SQL Server on an Azure Virtual Machine](https://azuremarketplace.microsoft.com/marketplace/apps?search=sql%20server&page=1), install SQL Server 2016, or update your existing instance. Similarly, you can also create a new virtual machine in Azure using SQL Server 2016 platform image.

  
###  <a name="bkmk_Limitations"></a> Limitations  
  
-   In the current release of this feature, storing **FileStream** data in Azure Storage is not supported. You can store **FileStream** data in a database that also contains data files stored in Azure storage, but all FileStream data files must be stored on local storage.  Since the FileStream data must reside on local storage, it cannot be moved between machines using Azure Storage, therefore we recommend that you continue using the [traditional techniques](../../relational-databases/blob/move-a-filestream-enabled-database.md) to move the data associated with FileStream between different machines.  
  
-   Currently, this new enhancement does not support more than one SQL Server instance accessing the same database files in  Azure Storage at the same time. If ServerA is online with an active database file and if ServerB is accidently started, and it also has a database which points to the same data file, the second server will fail to start the database with an error code **5120 Unable to open the physical file "%.\*ls". Operating system error %d: "%ls"**.  
  
-   Only .mdf, .ldf, and .ndf files can be stored in Azure Storage by using the SQL Server Data Files in Azure feature.  
  
-   When using the SQL Server Data Files in Azure feature, geo-replication for your storage account is not supported. If a storage account is geo-replicated and a geo-failover happened, database corruption could occur.  
  
-   For capacity limitations, see [Introduction to Blob storage](https://docs.microsoft.com/azure/storage/blobs/storage-blobs-introduction).  
  
-   It is not possible to store In-Memory OLTP data in Azure Blob using the SQL Server Data Files in Azure Storage feature. This is because In-Memory OLTP has a dependency on **FileStream** and, in the current release of this feature, storing **FileStream** data in Azure Storage is not supported.  
  
-   When using SQL Server Data Files in Azure feature, SQL Server performs all URL or file path comparisons using the Collation set in the **master** database.  
  
-   **Always On availability groups** are supported as long as you do not add new database files to the primary database. If a database operation requires a new file to be created in the primary database, first disable Always On availability groups in the secondary node. Then, perform the database operation on the primary database and backup the database in the primary node. Next, restore the database to the secondary node, and enable Always On availability groups in the secondary node. Note that Always On failover cluster instances is not supported when using the SQL Server data files in Azure feature.  
  
-   During normal operation, SQL Server uses temporary leases to reserve Blobs for storage with a renewal of each Blob lease every 45 to 60 seconds. If a server crashes and another instance of SQL Server configured to use the same blobs is started, the new instance will wait up to 60 seconds for the existing lease on the Blob to expire. If you want to attach the database to another instance and you cannot wait for the lease to expire within 60 seconds, you can explicitly break the lease on the Blob to avoid any failures in attach operations.  
  
## Tools and programming reference support  
 This section describes which tools and programming reference libraries can be used when storing SQL Server data files in Azure Storage.  
  
### PowerShell support  
 Use PowerShell cmdlets to store SQL Server data files in Azure Blob Storage service by referencing a Blob Storage URL path instead of a file path. Access Blobs using the following URL format: `https://storageaccount.blob.core.windows.net/<container>/<blob>` .  
  
### SQL Server object and performance counters support  
 Starting with SQL Server 2014, a new SQL Server object has been added to be used with SQL Server Data Files in Azure Storage feature. The new SQL Server object is called as [SQL Server, HTTP_STORAGE_OBJECT](../../relational-databases/performance-monitor/sql-server-http-storage-object.md) and it can be used by System Monitor to monitor activity when running SQL Server with Windows Azure Storage.  
  
### SQL Server Management Studio support  
 SQL Server Management Studio allows you to use this feature via several dialog windows. For example, you can type the URL path of the storage container, such as > https://teststorageaccnt.blob.core.windows.net/testcontainer/ :
 
 as a **Path** in several dialog windows, such as **New Database**, **Attach Database**, and **Restore Database**. For more information, see [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md).  
  
### SQL Server Management Objects (SMO) support  
 When using the SQL Server Data Files in Azure feature, all SQL Server Management Objects (SMO) are supported. If an SMO object requires a file path, use the BLOB URL format instead of a local file path, such as `https://teststorageaccnt.blob.core.windows.net/testcontainer/`. For more information about SQL Server Management Objects (SMO), see [SQL Server Management Objects &#40;SMO&#41; Programming Guide](../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md) in SQL Server Books Online.  
  
### Transact-SQL support  
 This new feature has introduced the following change in the Transact-SQL surface area:  
  
-   A new **int** column, **credential_id**, in the **sys.master_files** system view. The **credential_id** column is used to enable Azure Storage enabled data files to be cross-referenced back to sys.credentials for the credentials created for them. You can use it for troubleshooting, such as a credential cannot be deleted when there is a database file which uses it.  
  
##  <a name="bkmk_Troubleshooting"></a> Troubleshooting for SQL Server Data Files in Microsoft Azure  
 To avoid errors due to unsupported features or limitations, first review [Limitations](../../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md#bkmk_Limitations).  
  
 The list of errors that you might get when using the SQL Server Data Files in Azure Storage feature are as follows.  
  
 **Authentication errors**  
  
-   *Cannot drop the credential '%.\*ls' because it is used by an active database file.*   
    Resolution: You may see this error when you try to drop a credential that is still being used by an active database file in Azure Storage. To drop the credential, first you must delete the associated blob that has this database file. To delete a blob that has an active lease, you must first break the lease.  
  
-   *Shared Access Signature has not been created on the container correctly.*   
     Resolution: Make sure that you have created a Shared Access Signature on the container correctly. Review the instructions given in Lesson 2 in [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases ](../lesson-2-create-a-sql-server-credential-using-a-shared-access-signature.md).  
  
-   *SQL Server credential has not been not created correctly.*   
    Resolution: Make sure that you have used 'Shared Access Signature' for the **Identity** field and created a secret correctly. Review the instructions given in Lesson 3 in [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases](../lesson-3-database-backup-to-url.md).  
  
 **Lease blob errors:**  
  
-   Error when trying to start SQL Server after another instance using the same blob files has crashed. Resolution: During normal operation, SQL Server uses temporary leases to reserve Blobs for storage with a renewal of each Blob lease every 45 to 60 seconds. If a server crashes and another instance of SQL Server configured to use the same blobs is started, the new instance will wait up to 60 seconds for the existing lease on the Blob to expire. If you want to attach the database to another instance and you cannot wait for the lease to expire within 60 seconds, you can explicitly break the lease on the Blob to avoid any failures in attach operations.  
  
 **Database errors**  
  
1.  *Errors when creating a database*   
    Resolution: Review the instructions given in Lesson 4 in [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases](../lesson-4-restore-database-to-virtual-machine-from-url.md).  
  
2.  *Errors when running the Alter statement*   
    Resolution: Make sure to execute the Alter Database statement when the database is online. When copying the data files to Azure Storage, always create a page blob not a block blob. Otherwise, ALTER Database will fail. Review the instructions given in Lesson 7 in [Tutorial: Using the Microsoft Azure Blob storage service with SQL Server 2016 databases](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md).  
  
3.  *Error code 5120 Unable to open the physical file "%.\*ls". Operating system error %d: "%ls"*   
    Resolution: Currently, this new enhancement does not support more than one SQL Server instance accessing the same database files in Azure Storage at the same time. If ServerA is online with an active database file and if ServerB is accidently started, and it also has a database which points to the same data file, the second server will fail to start the database with an error *code 5120 Unable to open the physical file "%.\*ls". Operating system error %d: "%ls"*.  
  
     To resolve this issue, first determine if you need ServerA to access the database file in Azure Storage or not. If not, simply remove any connection between ServerA and the database files in Azure Storage. To do this, follow these steps:  
  
    1.  Set the file path of Server A to a local folder by using the ALTER Database statement.  
  
    2.  Set the database offline in Server A.  
  
    3.  Then, copy database files from Azure Storage to the local folder in Server A. This ensures that ServerA still has a copy of the database locally.  
  
    4.  Set the database online.  

## Next steps  
  
[Create a database](create-a-database.md)
