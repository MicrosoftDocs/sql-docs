---
title: Parallel Data Warehouse Components
description: This article explains the appliance software and the non-appliance software components of Analytics Platform System.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 02/26/2026
ms.service: sql
ms.subservice: data-warehouse
ms.topic: concept-article
---

# Parallel Data Warehouse components - Analytics Platform System

This article explains the appliance software and the non-appliance software components of Analytics Platform System.  

:::image type="content" source="media/parallel-data-warehouse-software.png" alt-text="Parallel Data Warehouse software." lightbox="media/parallel-data-warehouse-software.png":::  

<a id="sec1"></a>

## Appliance Software - Query Processing and User Data Storage

### Control Node

The MPP Engine is the brains of the Massively Parallel Processing (MPP) system. It does the following:  

- Creates parallel query plans and coordinates parallel query execution on the Compute nodes.  

- Stores and coordinates metadata and configuration data for all of the databases.  

- Manages SQL Server Parallel Data Warehouse (PDW) database authentication and authorization.  

- Tracks hardware and software status.  

### Data Movement Service (DMS)

Data Movement Service (DMS) is part of the "secret sauce" of PDW. It does the following:  

- Transfers data to and from the SQL Server PDW nodes.  

- Processes query operations that require transferring data among the nodes.  

- Improves query performance by optimizing data transfer speeds.  

### Admin Console

The Admin Console is a web application that presents the appliance state, health, and performance information.  

### Configuration Manager

The Configuration Manager (`dwconfig.exe`), is the tool that appliance administrators use to configure Analytics Platform System.  

### Control node databases

SQL Server manages all of the databases on the Control node.  

- The Shell database manages the metadata for all distributed user databases.  

- `TempDB` contains the metadata for all user temporary tables across the appliance.  

- `Master` is the `master` table for SQL Server on the Control node.  

### Compute Node

The Compute nodes are parallel data processing and storage units. They have direct attached storage and use SQL Server to manage user data.  

### Data Movement Service (DMS)

Data Movement Service (DMS) runs on each Compute node to do the following:  

- As part of processing parallel queries, DMS transfer data to and from other Computer nodes and the Control node.  

- DMS, running on each Compute node, receives data loads in parallel. Data is loaded in parallel directly from the loading server to the Compute nodes  

- DMS transfers data from each Compute node directly to the backup server.  

- Using PolyBase, DMS transfers data to and from an external Hadoop cluster or Azure Storage Blob.  

### Compute node databases

Each Compute node runs an instance of SQL Server to process queries and manage user data.  

## Appliance Fabric

The appliance fabric provides the operating system, services, and network infrastructure for the appliance.  

### Domain Controller

Active Directory (AD) Domain Services (DS)  
Analytics Platform System performs authentication among the Analytics Platform System nodes, and manages the authentication of SQL Server PDW Windows Authentication logins.  

DNS service  
Windows Domain Name Service (DNS) resolves domain names to IP addresses for the Analytics Platform System appliance.  

### Windows Deployment Service

Windows Deployment Service (WDS) deploys the Windows Server operating system onto the appliance. It is deployed on every host and virtual machine across the appliance.  

The DHCP service creates IP addresses so that the hosts within the appliance domain can join the appliance network without having a pre-configured IP address.  

### Virtual Machine Manager

Analytics Platform System uses virtualization to achieve high availability. The Virtual Machine Manager hosts System Center to deploy the operating system on the physical hosts.  

Windows Server Update Services (WSUS) to apply or remove Windows Updates across all of the hosts and virtual machines.  

### Windows Server

All of the hosts and virtual machines in the appliance run Windows Server operating system.  

### Failover Clustering

Windows Failover Clustering provides the ability to restart processes on a passive host if a host fails.  

### Storage Spaces

Windows Storage Spaces manages user data as a storage pool for a small group of Compute nodes. If a Compute node fails, the data is still accessible through another Compute node in the group.  

### Hyper-V

Microsoft Hyper-V Server provides a simple and reliable virtualization solution. Analytics Platform System uses virtualizations to balance CPU resources and to provide high availability for the PDW nodes and appliance fabric components.  

<a id="sec2"></a>

## Non-relational data

PolyBase technology integrates SQL Server PDW data with external Hadoop data. The Hadoop data can be stored on any of these Hadoop Data sources:  

- Hortonworks Hadoop Distribution  

- Cloudera Distribution of Hadoop  

- HDInsight data stored on Azure Storage Blob  

## Query Tools

Queries are written with Transact-SQL modified to fit the MPP nature of the queries. All queries are submitted to the Control node, which generates a parallel query plan to run the query across the Compute nodes.  

### SQL Server Data Tools (SSDT)

SQL Server Data Tools runs inside of Visual Studio and is our recommended GUI tool for submitting queries to SQL Server PDW. It is similar to SQL Server Management Studio by allowing you to navigate through an object explorer.  

If you don't already have Visual Studio, you can [download the tools that you need for free](https://visualstudio.microsoft.com/). 

### sqlcmd Command-Line Query Tool

`sqlcmd` is the SQL Server command-line tool for running Transact-SQL statements and system commands. It works with SQL Server PDW and is our recommended command-line tool for querying SQL Server PDW. With `sqlcmd` you can run Transact-SQL statements interactively from the command-line, as a batch file, or from Windows PowerShell.  

### Integration Services

You can use Integration Services to query SQL Server PDW. 

### Linked Server

By using a SQL Server linked server connection, you can use SQL Server to submit Transact-SQL statements to SQL Server PDW. 

## Business Intelligence Tools

### Analysis Services

SQL Server PDW is a valid data source for Analysis Services databases and Excel PowerPivot models. Using the OLE DB provider, you can configure an Analysis Services cube to use either multidimensional online analytical processing (MOLAP) or relational online analytical processing (ROLAP) storage.  

### Report Builder

You can use SQL Server PDW as a SQL Server data source for reports that you develop for Reporting Services by using SQL Server Report Builder. You can also use SQL Server PDW as a SQL Server source for report models. By using Report Manager or the report server API, you can generate a model from a SQL Server PDW database.  

### Power Pivot for Excel

You can connect to SQL Server PDW with PowerPivot for Excel, a free download that significantly expands the data analysis capabilities of Excel.  

<a id="loading-tools"></a>

## Load Tools

### Integration Services

Install Analytics Platform System (PDW)-specific destination adapters that allow you to use SQL Server Integration Services to load data into Analytics Platform System (PDW).  

### dwloader Command-Line Loader

`dwloader` is a command-line loading tool that loads data in parallel from your loading server to the SQL Server PDW Compute nodes. 

### PolyBase for Hadoop Integration

With PolyBase technology, you can load non-relational data from a Hadoop Cluster into a relational table in SQL Server PDW. The Hadoop data can be located in an external Hadoop Cluster or in Azure Blob Storage.  

## Database Backup and Restore

SQL Server PDW uses Transact-SQL database backup and restore commands to backup and restore user databases, in parallel, to and from a backup server. SQL Server PDW writes the backup to a directory in a Windows file share, and then likewise restores data from a Windows file share.  

For more information, see [Backup and loading hardware overview - Parallel Data Warehouse](backup-and-loading-hardware.md) and [Backup and restore](backup-and-restore-overview.md)  

## Remote Table Copy

The Remote Table Copy feature allows you to copy tables from SQL Server PDW databases to remote (non-appliance) SMP SQL Server databases. This enables hub and spoke scenarios for SQL Server PDW.  

<a id="monitoring"></a>

## Monitor

Analytics Platform System has several ways to monitor appliance activity. For more information on monitoring, see [Appliance Monitoring](appliance-monitoring.md).

### Admin Console

The Admin Console allows you to view current status about the appliance health. This runs as a web application on the Control node and is accessible over https.  

For more information, see [Monitor the appliance with the Admin Console - Analytics Platform System](monitor-the-appliance-by-using-the-admin-console.md)  

### System Views

The Admin Console is based on system view queries. You can query the system views individually to get the specific piece of information you need.

For more information, see [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md) 

### System Center Operations Manager

There are System Center Operations Manager Management Packs for SQL Server PDW. 

To configure the appliance for System Center Operations Manager, see [Monitor with System Center Operations Manager - Analytics Platform System](monitor-the-appliance-by-using-system-center-operations-manager.md)  

#### Management tasks

Review documentation and recommendations for common management tasks:

- [Appliance Installation and Configuration Overview (Analytics Platform System)](appliance-installation-and-configuration-overview.md)
- [Connect to Appliance Nodes (Analytics Platform System)](connect-to-appliance-nodes.md)
- [Antivirus Software (Analytics Platform System)](antivirus-software.md)
- [Appliance Configuration (Analytics Platform System)](appliance-configuration.md)
- [Software Servicing (Analytics Platform System)](software-servicing.md)
- [PDW Services Status (Analytics Platform System)](pdw-services-status.md)
- [Power the APS Appliance On or Off (Analytics Platform System)](power-the-aps-appliance-on-or-off.md)

## T-SQL support

To view T-SQL reference articles for PDW, select the **Analytics Platform System (PDW)** product version from the version dropdown list.

:::image type="content" source="media/version-selector.png" alt-text="Screenshot from learn.microsoft.com of the product Version selector.":::

For example, view the APS version of the [Collations and Unicode Support](../t-sql/statements/collations.md?view=aps-pdw-2016-au7&preserve-view=true) document.