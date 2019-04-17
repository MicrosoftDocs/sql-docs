---
title: "Configure File System Permissions for Database Engine Access | Microsoft Docs"
ms.custom: ""
ms.date: "06/06/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "file system permissions"
  - "service account [SQL Server], file system permissions"
  - "permissions [SQL Server], file system"
ms.assetid: 78bba43c-4edb-4216-84ac-d6246ae5546d
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure File System Permissions for Database Engine Access
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to grant the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], file system access to the location where database files are stored. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] service must have permission of the Windows file system to access the file folder where database files are stored. Permission to the default location is configured during setup. If you place your database files in a different location, you might need to follow these steps to grant the [!INCLUDE[ssDE](../../includes/ssde-md.md)] the full control permission to that location.  
  
 Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] permissions are assigned to the per-service SID for each of its services. This system helps provide service isolation and defense in depth. The per-service SID is derived from the service name and is unique to each service. The topic [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md) describes the per-service SID and provides the names in the section **Windows Privileges and Rights**. It is the per-service SID that must be assigned the access permission on the file location.  
  
## To Grant File System Permission to the Per-service SID  
  
1.  Using Windows Explorer, navigate to the file system location where the database files are stored. Right-click the file system folder, and then click **Properties**.  
  
2.  On the **Security** tab, click **Edit**, and then **Add**.  
  
3.  In the **Select Users, Computer, Service Account, or Groups** dialog box, click **Locations**, at the top of the location list, select your computer name, and then click **OK**.  
  
4.  In the **Enter the object names to select** box, type the name of the per-service SID name listed in the Books Online topic [**Configure Windows Service Accounts and Permissions**](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). (For the [!INCLUDE[ssDE](../../includes/ssde-md.md)] per service SID name, use **NT SERVICE\MSSQLSERVER** for a default instance, or **NT SERVICE\MSSQL$InstanceName** for a named instance.)  
  
5.  Click **Check Names** to validate the entry. (If the validation fails, it might advise you that the name was not found. When you click **OK**, a **Multiple Names Found** dialog box appears. Now select the per-service SID name, either **MSSQLSERVER** or **NT SERVICE\MSSQL$InstanceName**, and then click **OK**.  Click **OK** again to return to the **Permissions** dialog box.)   
6.  In the **Group or user** names box, select the per-service SID name, and then in the **Permissions for** \<name> box, select the **Allow** check box for **Full control**.  
  
7. Click **Apply**, and then click **OK** twice to exit.  
  
## See Also  
 [Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md)   
 [Move System Databases](../../relational-databases/databases/move-system-databases.md)   
 [Move User Databases](../../relational-databases/databases/move-user-databases.md)  
  
  
