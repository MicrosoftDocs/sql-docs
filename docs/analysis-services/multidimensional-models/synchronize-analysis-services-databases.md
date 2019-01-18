---
title: "Synchronize Analysis Services Databases | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Synchronize Analysis Services Databases
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] includes a database synchronization feature that makes two [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases equivalent by copying the data and metadata a database on a source server to a database on a destination server. Use the Synchronize Database feature to accomplish any of the following tasks:  
  
-   Deploy a database from a staging server onto a production server.  
  
-   Update a database on a production server with the changes made to the data and metadata in a database on a staging server.  
  
-   Generate XMLA script that can be run in the future to synchronize the databases.  
  
-   In distributed workloads where cubes and dimensions are processed on multiple servers, use database synchronization to merge the changes into a single database.  
  
 Database synchronization is initiated on the destination server, pulling data and metadata into a database copy on the source server. If the database does not exist, it will be created. Synchronization is a one-way, one-time operation that concludes once the database is copied. It does not provide real-time parity between the databases.  
  
 You can re-sync databases that already exist on source and destination servers to pull the latest changes from a staging server into a production database. Files on the two servers will be compared for changes and those that are different will be updated. An existing database on a destination server remains available while synchronization occurs in the background. Users can continue to query the destination database while synchronization is in progress. After synchronization finishes, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] automatically switches the users to the newly copied data and metadata, and drops the old data from the destination database.  
  
 To synchronize databases, run the Synchronize Database Wizard to immediately synchronize the databases, or use it to generate a synchronization script that you can run later. Either approach can be used to increase the availability and scalability of your [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases and cube.  
  
> [!NOTE]  
>  The following whitepapers, written for previous versions of Analysis Services, still apply to scalable multidimensional solutions built using SQL Server 2012. For more information, see [Scale-Out Querying with Analysis Services](http://go.microsoft.com/fwlink/?LinkId=253136) and [Scale-Out Querying for Analysis Services with Read-Only Databases](http://go.microsoft.com/fwlink/?LinkId=253137.)  
  
## Prerequisites  
 On the destination (or target) server from which you initiate database synchronization, you must be a member of the Analysis Services server administrator role. On the source server, your Windows user account must have Full Control permissions on the source database. If you are synchronizing database interactively, remember that synchronization runs under the security context of your Windows user identity. If your account is denied access to specific objects, those objects will be excluded from the operation. For more information about server administrator roles and database permissions, see [Grant server admin rights to an  Analysis Services instance](../../analysis-services/instances/grant-server-admin-rights-to-an-analysis-services-instance.md) and [Grant database permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/grant-database-permissions-analysis-services.md).  
  
 TCP port 2383 must be open on both servers to allow remote connections between default instances. For more information about creating an exception in Windows Firewall, see [Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md).  
  
 Both the source and destination servers must be the same version and service pack. Because model metadata is also synchronized, to ensure compatibility the build number for both servers should be the same. The edition of each installation must support database synchronization. In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], database synchronization is supported in enterprise, developer, and business intelligence editions. For more information about features in each edition, see [Editions and Supported Features for SQL Server 2016](../../sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 The server deployment mode must be identical on each server. If the database you are synchronizing is multidimensional, both source and destination servers must be configured for multidimensional server mode. For more information about deployment modes, see [Determine the Server Mode of an Analysis Services Instance](../../analysis-services/instances/determine-the-server-mode-of-an-analysis-services-instance.md).  
  
 Turn off lazy aggregation processing if you are using it on the source server. Aggregations that are processing in the background can interfere with database synchronization. For more information about setting this server property, see [OLAP Properties](../../analysis-services/server-properties/olap-properties.md).  
  
> [!NOTE]  
>  Database size is a factor in determining whether synchronization is a suitable approach. There are no hard requirements, but if synchronization is too slow, consider synchronizing multiple servers in parallel, as described in this technical paper: [Analysis Services Synchronization Best Practices](http://go.microsoft.com/fwlink/?LinkID=253136).  
  
## Synchronize Database Wizard  
 Use the Synchronize Database Wizard to perform one-way synchronization from a source to a destination database, or to generate a script that specifies a database synchronization operation. You can synchronize both local and remote partitions during the synchronization process and choose whether to include roles.  
  
 The Synchronize Database Wizard guides you through the following steps:  
  
-   Select the source instance and database from which to synchronize.  
  
-   Select storage locations for local partitions on the destination instance.  
  
-   Select storage locations for remote partitions on other destination instances.  
  
-   Select the level of security and membership information to copy from the source instance and database to the destination instance.  
  
-   Select whether to synchronize immediately or to save the XML for Analysis (XMLA) **Synchronize** command generated by the Synchronize Database Wizard to a script file for later synchronization.  
  
 By default, the wizard synchronizes all data and metadata, other than membership in existing security groups. You can also copy all security settings or ignore all security settings when synchronizing the data and metadata.  
  
#### Run the wizard  
  
1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that will run the destination database. For example, if you are deploying a database to a production server, you would run the wizard on the production server.  
  
2.  In Object Explorer, right-click the **Databases** folder, then click **Synchronize**.  
  
3.  Specify the source server and source database. In the Select Database to Synchronize page, in **Source Server** and **Source Database**, type the name of the source server and source database. For example, if you are deploying from a test environment to a production server, the source is the database on the staging server.  
  
     **Destination Server** displays the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance with which the data and metadata from the database selected in **Source database** is synchronized.  
  
     Synchronization will occur for source and destination databases that have the same name. If the destination server already has a database that shares the same name as the source database, the destination database will be updated with the metadata and data of the source. If the database does not exist, it will be created on the destination server.  
  
4.  Optionally, change location for local partition. Use the **Specify Locations for Local Partitions** page to indicate where local partitions should be stored on the destination server.  
  
    > [!NOTE]  
    >  This page appears only if at least one local partition exists in the specified database.  
  
     If a set of partitions are installed on drive C of the source server, the wizard lets you copy this set of partitions to a different location on the destination server. If you do not change the default locations, the wizard deploys the measure group partitions within each cube on the source server to the same locations on the destination server. Similarly, if the source server uses remote partitions, the same remote partitions will be used on the destination server.  
  
     The **Locations** option displays a grid listing the source folder, destination folder, and estimated size of the local partitions to be stored on the destination instance. The grid contains the following columns:  
  
     **Source Folder**  
     Displays the folder name on the source [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance that contains the local partition. If the column contains the value "(Default)", the default location for the source instance contains the local partition.  
  
     **Destination Folder**  
     Displays the folder name on the destination [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance into which the local partition is to be synchronized. If the column contains the value, "(Default)", the default location for the destination instance contains the local partition.  
  
     Click the ellipsis (**...**) button to display the **Browse for Remote Folder** dialog box and specify a folder on the destination instance into which the local partitions stored in the selected location should be synchronized.  
  
    > [!NOTE]  
    >  This column cannot be changed for local partitions stored in the default location for the source instance.  
  
     **Size**  
     Displays the estimated size of the local partition.  
  
     The **Partitions in selected location** option displays a grid that describes the local partitions stored in the location on the source [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance specified in the **Source Folder** column of the selected row in **Locations**.  
  
     **Cube**  
     Displays the name of the cube that contains the partition.  
  
     **Measure Group**  
     Displays the name of the measure group in the cube that contains the partition.  
  
     **Partition Name**  
     Displays the name of the partition.  
  
     **Size(Mb)**  
     Displays the size in megabytes (MB) of the partition.  
  
5.  Optionally, change location for remote partitions.Use the **Specify Locations for Remote Partitions** page to indicate if remote partitions managed by the specified database on the source server should be synchronized, and to specify a destination [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance and database into which the selected remote partitions should be stored.  
  
    > [!NOTE]  
    >  This page appears only if at least one remote partition is managed by the specified database on the source [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance.  
  
     The **Locations** option displays a grid that lists details about locations in which remote partitions for the source database are stored, including source and destination information and the storage size used by each location, available from the selected database. The grid contains the following columns:  
  
     **Sync**  
     Select to include a location that contains the remote partitions during synchronization.  
  
    > [!NOTE]  
    >  If this option is not selected for a location, remote partitions that are contained in that location will not be synchronized.  
  
     **Source Server**  
     Displays the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance that contains remote partitions.  
  
     **Source Folder**  
     Displays the folder name on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance that contains remote partitions. If the column contains the value "(Default)", the default location for the instance displayed in **Source Server** contains remote partitions.  
  
     **Destination Server**  
     Displays the name of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance into which the remote partitions stored in the location specified in **Source Server** and **Source Folder** should be synchronized.  
  
     Click the ellipsis (**...**) button to display the **Connection Manager** dialog box and specify an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance into which the remote partitions stored in the selected location should be synchronized.  
  
     **Destination Folder**  
     Displays the folder name on the destination [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance into which the remote partition is to be synchronized. If the column contains the value, "(Default)", the default location for the destination instance should contain the remote partition.  
  
     Click the ellipsis (**...**) button to display the **Browse for Remote Folder** dialog box and specify a folder on the destination instance into which the remote partitions stored in the selected location should be synchronized.  
  
     **Size**  
     Displays the estimated size of remote partitions stored in the location.  
  
     The **Partitions in selected location** displays a grid that describes the remote partitions stored in the location on the source [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance specified in the **Source Folder** column of the selected row in **Locations**. The grid contains the following columns:  
  
     **Cube**  
     Displays the name of the cube that contains the partition.  
  
     **Measure Group**  
     Displays the name of the measure group in the cube that contains the partition.  
  
     **Partition Name**  
     Displays the name of the partition.  
  
     **Size(Mb)**  
     Displays the size in megabytes (MB) of the partition.  
  
6.  Specify whether user permission information should be included, and whether compression should be used. By default, the wizard compresses all data and metadata prior to copying the files to the destination server. This option results in a faster file transmission. Files are uncompressed once they reach the destination server.  
  
     **Copy all**  
     Select to include security definitions and membership information during synchronization.  
  
     **Skip membership**  
     Select to include security definitions, but exclude membership information, during synchronization.  
  
     **Ignore all**  
     Select to ignore the security definition and membership information currently in the source database. If a destination database is created during synchronization, no security definitions or membership information will be copied. If the destination database already exists and has roles and memberships, that security information will be preserved.  
  
7.  Choose the synchronization method. You can synchronize immediately or generate a script that is saved to a file. By default, the file is saved with an .xmla extension and placed in your Documents folder.  
  
8.  Click **Finish** to synchronize. After verifying the options on the **Completing the Wizard** page, click **Finish** again.  
  
## Next Steps  
 If you did not synchronize roles or membership, remember to specify user access permissions now on the destination database.  
  
## See Also  
 [Synchronize Element &#40;XMLA&#41;](https://docs.microsoft.com/bi-reference/xmla/xml-elements-commands/synchronize-element-xmla)   
 [Deploy Model Solutions Using XMLA](../../analysis-services/multidimensional-models/deploy-model-solutions-using-xmla.md)   
 [Deploy Model Solutions Using the Deployment Wizard](../../analysis-services/multidimensional-models/deploy-model-solutions-using-the-deployment-wizard.md)  
  
  
