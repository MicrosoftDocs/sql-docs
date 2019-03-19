---
title: "Configuring Storage Spaces with a NVDIMM-N write-back cache | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: 861862fa-9900-4ec0-9494-9874ef52ce65
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Configuring Storage Spaces with a NVDIMM-N write-back cache
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Windows Server 2016 supports NVDIMM-N devices that allow for extremely fast input/output (I/O) operations. One attractive way of using such devices is as a write-back cache to achieve low write latencies. This topic discusses how to set up a mirrored storage space with a mirrored NVDIMM-N write-back cache as a virtual drive to store the SQL Server transaction log. If you are looking to utilize it to also store data tables or other data, you may include more disks in the storage pool, or create multiple pools, if isolation is important.  
  
 To view a Channel 9 video using this technique, see [Using Non-volatile Memory (NVDIMM-N) as Block Storage in Windows Server 2016](https://channel9.msdn.com/Events/Build/2016/P466).  
  
## Identifying the right disks  
 Setup of storage spaces in Windows Server 2016, especially with advanced features, such as write-back caches is most easily achieved through PowerShell. The first step is to identify which disks should be part of the Storage Spaces pool that the virtual disk will be created from. NVDIMM-Ns have a media type and bus-type of SCM (storage class memory) , which can be queried via the Get-PhysicalDisk PowerShell cmdlet.  
  
```  
Get-PhysicalDisk | Select FriendlyName, MediaType, BusType  
```  
  
 ![Get-PhysicalDisk](../../relational-databases/performance/media/get-physicaldisk.png "Get-PhysicalDisk")  
  
> [!NOTE]  
>  With NVDIMM-N devices, you no longer need to specifically select the devices that can be write-back cache targets.  
  
 In order to build a mirrored virtual disk with mirrored write-back cache, at least 2 NVDIMM-Ns, and 2 other disks are needed. Assigning the desired physical disks to a variable before building the pool makes the process easier.  
  
```  
$pd =  Get-PhysicalDisk | Select FriendlyName, MediaType, BusType | WHere-Object {$_.FriendlyName -like 'MK0*' -or $_.FriendlyName -like '2c80*'}  
```  
  
 The screenshot shows the $pd variable and the 2 SSDs and 2 NVDIMM-Ns it is assigned to returned using the following PowerShell cmdlet.  
  
```  
$pd | Select FriendlyName, MediaType, BusType  
```  
  
 ![Select FriendlyName](../../relational-databases/performance/media/select-friendlyname.png "Select FriendlyName")  
  
## Creating the Storage Pool  
 Using the $pd variable containing the PhysicalDisks, it is easy to build the storage pool using the New-StoragePool PowerShell cmdlet.  
  
```  
New-StoragePool -StorageSubSystemFriendlyName "Windows Storage*" -FriendlyName NVDIMM_Pool -PhysicalDisks $pd  
```  
  
 ![New-StoragePool](../../relational-databases/performance/media/new-storagepool.png "New-StoragePool")  
  
## Creating the Virtual Disk and Volume  
 Now that a pool has been created, the next step is to carve out a virtual disk and format it. In this case only 1 virtual disk will be created and the New-Volume PowerShell cmdlet can be used to streamline this process:  
  
```  
New-Volume -StoragePool (Get-StoragePool -FriendlyName NVDIMM_Pool) -FriendlyName Log_Space -Size 300GB -FileSystem NTFS -AccessPath S: -ResiliencySettingName Mirror  
```  
  
 ![New-Volume](../../relational-databases/performance/media/new-volume.png "New-Volume")  
  
 The virtual disk has been created, initialized, and formatted with NTFS. The screen capture below shows that it has a size of 300GB and a write-cache size of 1GB, which will be hosted on the NVDIMM-Ns.  
  
 ![Get-VirtualDisk](../../relational-databases/performance/media/get-virtualdisk.png "Get-VirtualDisk")  
  
 You can now view this new volume visible in your server. You can now use this drive for your SQL Server transaction log.  
  
 ![Log_Space Drive](../../relational-databases/performance/media/log-space-drive.png "Log_Space Drive")  
  
## See Also  
 [Windows Storage Spaces in Windows 10](https://windows.microsoft.com/windows-10/storage-spaces-windows-10)   
 [Windows Storage Spaces in Windows 2012 R2](https://technet.microsoft.com/library/hh831739.aspx)   
 [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md)   
 [View or Change the Default Locations for Data and Log Files &#40;SQL Server Management Studio&#41;](../../database-engine/configure-windows/view-or-change-the-default-locations-for-data-and-log-files.md)  
  
  
