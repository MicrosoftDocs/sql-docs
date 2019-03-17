---
title: "Create and Manage a Remote Partition (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "partitions [Analysis Services], remote"
  - "remote partitions [Analysis Services]"
ms.assetid: 4322b5cb-af07-4e79-8ecb-59e1121a9eb8
author: minewiskan
ms.author: owend
manager: craigg
---
# Create and Manage a Remote Partition (Analysis Services)
  When partitioning a measure group, you can configure a secondary database on a remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance as partition storage.  
  
 Remote partitions for a cube (called the master database) are stored in a dedicated [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database on the remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] (called the secondary database).  
  
 A dedicated secondary database can store remote partitions for one and only one master database, but the master database can use multiple secondary databases, as long as all the secondary databases are on the same remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. Dimensions in a database dedicated to remote partitions are created as linked dimensions.  
  
## Prerequisites  
 Before you create a remote partition, the following conditions must be met:  
  
-   You must have a second [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and dedicated database to store the partitions. The secondary database is single-purpose; it provides storage of remote partitions for a master database.  
  
-   Both server instances must be the same version. Both databases should be the same functional level.  
  
-   Both instances must be configured for TCP connections. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] does not support creation of remote partitions by using the HTTP protocol.  
  
-   Firewall settings on both computers must be set to accept outside connections. For information about setting the firewall, see [Configure the Windows Firewall to Allow Analysis Services Access](../instances/configure-the-windows-firewall-to-allow-analysis-services-access.md).  
  
-   The service account for the instance running the master database must have administrative access to the remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. If the service account changes, you must update permissions on both the server and database.  
  
-   You must be an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] administrator on both computers.  
  
-   You must ensure your disaster recovery plan accommodates backup and restore of the remote partitions. Using remote partitions can complicate backup and restore operations. Be sure to test your plan thoroughly to be sure you can restore the necessary data.  
  
## Configure remote partitions  
 Two separate computers that are running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] are each required to create a remote partition arrangement that designates one computer as the master server and the other computer as the subordinate server.  
  
 The following procedure assumes that you have two server instances, with a cube database deployed on the master server. For the purposes of this procedure, the cube database is referred to as db-master. The storage database containing remote partitions is referred to as db-storage.  
  
 You will use both [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to complete this procedure.  
  
> [!NOTE]  
>  Remote partitions can be merged only with other remote partitions. If you are using a combination of local and remote partitions, an alternative approach is to create new partitions that include the combined data, deleting the partitions you no longer use.  
  
#### Specify valid server names for cube deployment (in SSDT)  
  
1.  On the master server: In Solution Explorer, right-click the solution name and select **Properties**. In the **Properties** dialog box, click **Configuration Properties**, then click **Deployment**, and then click **Server** and set the master server's name.  
  
2.  On the subordinate server: In Solution Explorer, right-click the solution name and select **Properties**. In the **Properties** dialog box, click **Configuration Properties**, then click **Deployment**, and then click **Server** and set the subordinate server's name.  
  
#### Create and deploy a secondary database (in SSDT)  
  
1.  On the subordinate server: Create a new Analysis Services project for the storage database.  
  
2.  On the subordinate server: In Solution Explorer, create a new data source pointing to the cube database, db-master. Use the provider **Native OLE DB\Microsoft OLE DB Provider for Analysis Services 11.0**.  
  
3.  On the subordinate server: Deploy the solution.  
  
#### Enable features (in SSMS)  
  
1.  On the subordinate server: In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click your connected [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance in Object Explorer and select **Properties**. Set both **Feature\LinkToOtherInstanceEnabled** and **Feature\LinkFromOtherInstanceEnabled** to **True**.  
  
2.  On the subordinate server: Restart the server by right-clicking the server name in Object Explorer and selecting **Restart**.  
  
3.  On the master server: In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], right-click your connected [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance in Object Explorer and select **Properties**. Set both **Feature\LinkToOtherInstanceEnabled** and **Feature\LinkFromOtherInstanceEnabled** to **True**.  
  
4.  On the master server: To restart the server, right-click the server name in Object Explorer and select **Restart**.  
  
#### Set the MasterDataSourceID database property on the remote server (in SSMS)  
  
1.  On the subordinate server: Right-click the storage database, db-storage, point to **Script Database as** | **ALTER To** | **New Query Editor Window**.  
  
2.  Add **MasterDataSourceID** to the XMLA, and then specify the cube database, db-master, ID as the value. The XMLA should look similar to the following.  
  
    ```  
    <Alter ObjectExpansion="ExpandFull" xmlns="https://schemas.microsoft.com/analysisservices/2003/engine">  
    <Object>  
       <DatabaseID>DB-Storage</DatabaseID>  
    </Object>  
    <ObjectDefinition>  
       <Database xmlns:xsd="http://www.w3.org/2001/XMLSchema" 400"   
          <ID>DB-Storage</ID>  
          <Name>DB-StorageB</Name>  
          <ddl200:CompatibilityLevel>1100</ddl200:CompatibilityLevel>  
          <Language>1033</Language>  
          <Collation>Latin1_General_CI_AS</Collation>  
          <DataSourceImpersonationInfo>  
    <ImpersonationMode>ImpersonateAccount</ImpersonationMode>  
             <Account>*********</Account>  
          </DataSourceImpersonationInfo>  
          <MasterDataSourceID>DB-Master</MasterDataSourceID>  
       </Database>  
    </ObjectDefinition>  
    </Alter>  
    ```  
  
3.  Press F5 to execute the script.  
  
#### Set up the remote partition (in SSDT)  
  
1.  On the master server: Open the cube in Cube Designer and click **Partitions** tab. Expand the measure group. Click **New Partition** if the measure group is already configured for multiple partitions, or click the browse (. . ) button in the Source column to edit the existing partition.  
  
2.  In the Partition Wizard, in **Specify Source Information**, select the original Data Source View and fact table.  
  
3.  If using a query binding, provide a WHERE clause that segments the data for the new partition you are creating.  
  
4.  In **Processing and Storage Locations**, under **Processing Location**, choose **Remote Analysis Services data source** and click **New** to create a new data source that points to your subordinate database, db-storage.  
  
    > [!NOTE]  
    >  If you get an error indicating the data source does not exist in the collection, you must open the project of the storage database, db-storage, and create a data source that points to the master database, db-master.  
  
5.  On the master server: Right-click the cube name in Solution Explorer, select **Process** and fully process the cube.  
  
## Administering remote partitions  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports both parallel and sequential processing of remote partitions. The master database, where the partitions were defined, coordinates the transactions among all the instances that participate in processing the partitions of a cube. Processing reports are then sent to all instances that processed a partition.  
  
 A cube that contains remote partitions can be administered together with its partitions on a single instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. However, the metadata for the remote partition can be viewed and updated only on the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] where the partition and its parent cube were defined. The remote partition cannot be viewed or updated on the remote instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
> [!NOTE]  
>  Although databases dedicated to storage of remote partitions are not exposed to schema rowsets, applications that use Analysis Management Objects (AMO) can still discover a dedicated database by using the XML for Analysis Discover command. Any CREATE or DELETE command that is sent directly to a dedicated database by using a TCP or HTTP client will succeed, but the server will return a warning indicating that the action may damage this closely managed database.  
  
## See Also  
 [Partitions &#40;Analysis Services - Multidimensional Data&#41;](../multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md)  
  
  
