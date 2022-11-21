---
title: Loading server capacity planning
description: This capacity planning worksheet helps you to determine the requirements for a loading server for loading data to Analytics Platform System Parallel Data Warehouse.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Loading server capacity planning worksheet for Analytics Platform System
This capacity planning worksheet helps you to determine the requirements for a loading server for loading data into SQL Server PDW. Use this to create your plan for purchasing or provisioning existing loading servers.

## Worksheet notes

1.  This worksheet applies to servers that will be loading data with the **dwloader** Command-Line Loading Tool.

2.  For loading data with Integration Services or a third party loading tool, the requirements can vary depending upon differences in the loading process.

3.  Most of the requirements apply to loading either compressed or uncompressed data files; any differences in requirements are noted in bold.

## ![Clipboard](media/clipboard-icon.png "Clipboard") Capacity Planning Worksheet

Print this worksheet and fill it in with your own requirements.

|Component|Requirement|Fill in this column with your own requirements|Recommendations|
|-------------|---------------|--------------------------------------------------|-------------------|
|Storage|Maximum bytes you plan to store on the Loading server at any given period of time.|![Pencil icon](media/pencil-icon.png "Pencil icon")|To determine storage requirements, figure out how much data you plan to store on the Loading server at any given period of time.  Capacity requirements are for load files only; the operating system and the load files should be on different disk arrays.<br /><br />For example: If you plan to load 100 GB of data from disk 3 times each day, but not delete the data files until the end of the week, then you need a minimum 2.1 TB to store the data files. We recommend being conservative and getting about 30% more storage to account for variances and growth.  For this example, 2.73 TB of storage space would be better.|
|Load Rate|Maximum bytes per hour of data to load into PDW.|![Pencil icon](media/pencil-icon.png "Pencil icon")|This is an estimate. When computing this requirement, assume that the files are already on the Loading server, and that other loading conditions are as good as possible.<br /><br />For example: No need to factor in data compressibility since dwloader always sends uncompressed data to the PDW. No need to factor in data type conversions and the size of the destination table.|
|Network|Network connection type.|![Pencil icon](media/pencil-icon.png "Pencil icon")|Determine the best network connection type for your load rate requirements.<br /><br />For example: InfiniBand or 10 Gbit Ethernet will provide the optimal loading rates. 1 Gbit Ethernet will limit load rates to 360 GB per hour or less.|
|I/O|Bytes per hour for reads and writes.|![Pencil icon](media/pencil-icon.png "Pencil icon")|To load data, dwloader must read all of the data from disk before sending it to PDW.<br /><br />Each loading server cannot load data faster than the appliance can receive data from all loading sources. To save money, plan the I/O read capacity for loading so that it does not exceed the load capacity of the appliance.<br /><br />For example:<br />PDW receives and loads data into a 1-rack appliance at a maximum rate of 1.8 TB per hour. For an appliance with 2 or more racks, the maximum load rate is 3.6 TB per hour.<br /><br />If you plan to load from multiple loading servers at the same time, the I/O requirements for each loading server will be less than when one server is doing all the loading.<br /><br />For example: One loading server can load a maximum of 1.8 TB per hour for a 1-rack appliance. Two loading servers could each concurrently load 900 GB per hour into a 1-rack appliance. Higher levels of concurrency can reduce efficiency and maximum throughput.<br /><br />For I/O capacity, take into account all of the I/O happening on the loading server. If the Loading server has other I/O traffic in addition to data loads, such as receiving data files from an ETL server, the I/O requirements will increase.<br /><br />For compressed data, the I/O requirements depend on the data compression rate. dwloader reads the compressed data and then uncompresses it before sending it to PDW. The higher the compression ratio, the less data the loading server will need to read from disk.<br /><br />For example: If the required load rate is 1.8 TB per hour, and the data is stored on the loading server with 2:1 compression, then the loading server needs only to read 900 GB per hour from disk instead of 1.8 TB. A 3:1 compression ratio means the loading server needs to read 600 GB per hour from disk.|
|CPU|Number of sockets.|![Pencil icon](media/pencil-icon.png "Pencil icon")|For loading uncompressed data, dwloader is not a CPU-intensive application.  As a minimum requirement, we recommend using a recently-manufactured 2-socket server.<br /><br />For loading compressed data, you need enough CPU power to uncompress the data before sending it to PDW. dwloader can run 10 active threads at once. If you plan to load 10 compressed files concurrently, we recommend the server has at least a 10-core CPU or two 6-core CPUs.|
|RAM|GB of memory that allows Windows to cache files during loads.|![Pencil icon](media/pencil-icon.png "Pencil icon")|dwloader uses very little RAM on the loading server. For performance, Windows uses memory to cache the loading files after reading them from disk.<br /><br />To determine the RAM requirements, refer to your Windows Server installation and any 3rd party application requirements. We recommend a minimum of 32 GB if you do not have requirements from other sources.<br /><br />For compressed data, faster RAM is useful because it will speed up the decompression.|

## See Also
[Acquire and configure a loading server](acquire-and-configure-loading-server.md)
[dwloader Command-Line Loader](dwloader.md)

