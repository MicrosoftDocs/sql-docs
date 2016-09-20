---
title: "CREATE EXTERNAL DATA SOURCE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ed13c686-a0ec-4dd6-8d18-07d36553b4bd
caps.latest.revision: 29
author: BarbKess
---
# CREATE EXTERNAL DATA SOURCE (SQL Server PDW)
Creates a SQL Server PDW external data source for use with PolyBase. The data are stored in Hadoop File System (HDFS) or Azure blob storage.  
  
To use PolyBase integration, you need to create an external data source, an external file format, and an external table. For more information, see [PolyBase &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/polybase-sql-server-pdw.md).  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
--Create an external data source for a Hadoop cluster  
CREATE EXTERNAL DATA SOURCE data_source_name  
    WITH (   
        TYPE = HADOOP,  
        LOCATION = 'hdfs://NameNode_URI[:port]'  
        [, JOB_TRACKER_LOCATION = 'JobTracker_URI[:port]' ]   
    )  
[;]  
  
--Create an external data source for Azure blob storage  
CREATE EXTERNAL DATA SOURCE data_source_name  
    WITH (   
        TYPE = HADOOP,  
        LOCATION = 'wasb[s]://[ container@ ] account_name.blob.core.windows.net/path'  
    )  
[;]  
```  
  
## Arguments  
*data_source_name*  
Specifies the user-defined name for the data source. The name must be unique for SQL Server PDW.  
  
TYPE = HADOOP  
Specifies the data source type. HADOOP is the only supported type in this release.  
  
LOCATION = 'hdfs://*NameNode_URI*[:*port*]'  
Specifies the Uniform Resource Indicator (URI) for a Hadoop external data source.  
  
> [!IMPORTANT]  
> The LOCATION value is a string and is not validated when you create the external data source. Entering an incorrect value can cause future delays when accessing the location. To avoid typing errors, we recommend to cut and paste the appropriate example and then replace the italicized parameters with the correct ones for your Hadoop cluster.  
  
Examples:  
  
-   HDInsight on APS:  
  
    Copy this code and replace *F12345* with your appliance domain. The –C specifies to use the InfiniBand network, and HHN01 is the name of the HDInsight Head node; these are constant and must be present.  
  
    ```  
    LOCATION = 'hdfs://F12345-C-HHN01:8020'  
    ```  
  
-   Hortonworks or Cloudera  
  
    Copy this code and replace *NameNode_IP* with the IP address of your Name node.  
  
    ```  
    LOCATION = 'hdfs://NameNode_IP:8020'  
    ```  
  
JOB_TRACKER_LOCATION = '*JobTracker_URI*[:*port*]'  
Specifies to push predicate computation to the Hadoop cluster. The port number for JOB_TRACKER_LOCATION cannot be the same as the port number for LOCATION. It is specific to the Hadoop distribution.  
  
> [!IMPORTANT]  
> For Hortonworks on HDP 2.0, JOB_TRACKER_LOCATION port is not optional. For HDP 2.0 on Windows, the port must be 8032. For HDP 2.0 on Linux, the port must be 8050.  
  
> [!IMPORTANT]  
> The JOB_TRACKER_LOCATION value is a string and is not validated when you create the external data source. Entering an incorrect value can cause future delays when accessing the location. To avoid typing errors, we recommend to cut and paste the appropriate example and then replace the italicized parameters with the correct ones for your Hadoop cluster.  
  
Examples:  
  
In these examples, replace *F12345* with your appliance domain_name, and replace *NameNode_IP* with the IP address of your Name node.  
  
-   HDI on APS with HDP 2.0:  
  
    ```  
    JOB_TRACKER_LOCATION = 'F12345-C-HHN01:8032'  
    ```  
  
-   HDI on APS with HDP 1.3:  
  
    ```  
    JOB_TRACKER_LOCATION = 'F12345-C-HHN01:50300'  
    ```  
  
-   Hortonworks HDP 2.0 on Windows:  
  
    ```  
    JOB_TRACKER_LOCATION = 'hdfs://NameNode_IP:8032'  
    ```  
  
-   Hortonworks HDP 1.3 on Windows:  
  
    ```  
    JOB_TRACKER_LOCATION = 'hdfs://NameNode_IP:50300'  
    ```  
  
-   Hortonworks HDP 2.0 on Linux:  
  
    ```  
    JOB_TRACKER_LOCATION = 'hdfs://NameNode_IP:8050'  
    ```  
  
-   Hortonworks HDP 1.3 on Linux:  
  
    ```  
    JOB_TRACKER_LOCATION = 'hdfs://NameNode_IP:50300'  
    ```  
  
-   Cloudera 4.3 on Linux:  
  
    ```  
    JOB_TRACKER_LOCATION = 'hdfs://NameNode_IP:8021'  
    ```  
  
LOCATION = 'wasb[s]://[ *container*@ ]*account_name*.blob.core.windows.net/*path*'  
Specifies the URI for connecting to Azure blob storage.  
  
wasb[s]  
Specifies the protocol for Azure blob storage. The [s] is optional and specifies a secure connection. We recommend using 'wasbs' instead of 'wasb'.  
  
> [!NOTE]  
> The location can use asv[s] instead of wasb[s]. The asv[s] syntax is deprecated and will be removed in a future release.  
  
*container*  
Specifies the name of the Azure blob storage container. To specify the root container of a domain’s storage account, use the domain name instead of the container name. Root containers are read-only, so data cannot be written back to the container.  
  
*account_name*  
The fully qualified domain name (FQDN) of the Azure storage account. Default is the default file system.  
  
*path*  
Specifies the HDFS path to the blob storage file. The path looks like a directory path and uses / as a directory separator. The path is actually stored as a key and value pair instead of a hierarchical path. A blob’s key-value pair of day1/log1.txt specifies the key as day1 and the file as log1.txt.  
  
Example:  
  
```  
LOCATION = 'wasbs://dailylogs@myaccount.blob.core.windows.net/'  
```  
  
## Permissions  
Requires CONTROL SERVER or ALTER ANY EXTERNAL DATA SOURCE permission.  
  
## Error Handling  
A runtime error will occur if the external Hadoop data sources are inconsistent about having JOB_TRACKER_LOCATION defined.  
  
SQL Server PDW does not verify the existence of the external data source when it creates the external data source object. If the data source does not exist during query execution, an error will occur.  
  
## General Remarks  
The external data source is stored in master as a server level object.  
  
When JOB_TRACKER_LOCATION is defined, SQL Server PDW will consider optimizing each query against the external data source by initiating a map reduce job on the Hadoop source. The results of the map reduce job will be transferred, in parallel, back to SQL Server PDW so that SQL Server PDW can continue to run the query.  
  
## Limitations and Restrictions  
For HDP 2.0 clusters, when you specify JOB_TRACKER_LOCATION, you must also specify the port.  
  
All data sources defined on the same Hadoop cluster location must use the same setting for JOB_TRACKER_LOCATION. If there is inconsistency, a runtime error will occur.  
  
For HDFS connections, if the Hadoop cluster is setup with a name and the external data source uses the IP address for the cluster location, SQL Server PDW must still be able to resolve the cluster name when the data source is used. To enable PDW to resolve the name, you can use a DNS forwarder. For more information, see [Use a DNS Forwarder to Resolve Non-Appliance DNS Names &#40;Analytics Platform System&#41;](../../mpp/management/use-a-dns-forwarder-to-resolve-non-appliance-dns-names-analytics-platform-system.md).  
  
## Locking  
Takes a shared lock on the EXTERNALDATASOURCE object.  
  
## Examples  
  
### A. Create an HDInsight on APS external data source  
This example creates an external data source for the HDI Region on APS.  LOCATION is the appliance domain name, followed by '-C-', and HHN01 which is the name of the HDI name node.  
  
> [!WARNING]  
> We recommend that you copy and paste this code and then replace F12345 with your appliance domain name. Specification mistakes in this code can cause significant delays.  
  
```  
CREATE EXTERNAL DATA SOURCE HDI_eds  
WITH (   
    TYPE = HADOOP,  
    LOCATION = 'hdfs://F12345-C-HHN01:8020',  
  
);  
```  
  
### B. Create an HDI on APS external data source with predicate pushdown  
This example creates an external data source for the HDI on APS. It is the same as the previous example, except that it uses predicate pushdown. When a PolyBase query selects from this external data source, some or all of the query predicate can be computed on Hadoop, which could reduce data transfer from Hadoop to PDW, and improve query performance .  
  
> [!WARNING]  
> We recommend that you copy and paste this code and then replace F12345 with your appliance domain name. Specification mistakes in this code can cause significant delays.  
  
```  
--For HDI Regions running Hortonworks HDP 2.0 on Windows  
CREATE EXTERNAL DATA SOURCE HDI_pp_eds  
WITH (   
    TYPE = HADOOP,  
    LOCATION = 'hdfs://F12345-C-HHN01:8020',  
    JOB_TRACKER_LOCATION = 'F12345-C-HHN01:8032'  
);  
  
--For HDI Regions running Hortonworks HDP 1.3  
CREATE EXTERNAL DATA SOURCE HDI_pp_eds  
WITH (   
    TYPE = HADOOP,  
    LOCATION = 'hdfs://F12345-C-HHN01:8020',  
    JOB_TRACKER_LOCATION = 'F12345-C-HHN01:50300'  
);  
```  
  
### C. Create a Hortonworks or Cloudera external data source  
To create a Hortonworks or Cloudera external data source, specify the IP address of the Hadoop name node.In this example, replace 10.10.10.10 with the IP address of your Hadoop name node, and then specify port 8020 which is the default port.  
  
```  
--Hortonworks or Cloudera external data source  
CREATE EXTERNAL DATA SOURCE hadoop_eds  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://10.10.10.10:8020'  
);  
```  
  
### D. Create a Hortonworks or Cloudera external data source with predicate pushdown  
These examples use the JOB_TRACKER_LOCATION option to specify that PolyBase queries will perform predicate pushdown when the query optimizer deems it to be advantageous.  
  
Each Hadoop distribution uses a different port for the job tracker location. These examples give the default port for each supported Hadoop distribution.  
  
In the following examples, the JOB_TRACKER_LOCATION uses ports 8050 and 50300 which are the job tracker ports for Hortonworks 2.0 on Linux and 1.3 on Windows or Linux respectively.  
  
```  
--Create an external data source to Hortonworks HDP 2.0 on Linux  
--and enable predicate pushdown.  
CREATE EXTERNAL DATA SOURCE Hortonworks_pp_eds  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://10.10.10.10:8020',  
    JOB_TRACKER_LOCATION = '10.10.10.10:8050'  
);  
  
--Create an external data source to Hortonworks HDP 2.0 on Windows  
--and enable predicate pushdown.  
CREATE EXTERNAL DATA SOURCE Hortonworks_pp_eds  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://10.10.10.10:8020',  
    JOB_TRACKER_LOCATION = '10.10.10.10:8032'  
);  
  
--Create an external data source to Hortonworks HDP 1.3 on Windows or Linux  
--and enable predicate pushdown.  
CREATE EXTERNAL DATA SOURCE Hortonworks_pp_eds  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://10.10.10.10:8020',  
    JOB_TRACKER_LOCATION = '10.10.10.10:50300'  
);  
```  
  
In this next example, the JOB_TRACKER_LOCATION uses port 8021 which is the default port for Cloudera 4.3.  
  
```  
--Create an external data source to Cloudera 4.3 on Linux  
--and enable predicate pushdown.  
CREATE EXTERNAL DATA SOURCE Cloudera_pp_eds  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://10.10.10.10:8020',  
    JOB_TRACKER_LOCATION = 10.10.10.10:8021'  
);  
```  
  
### D. Create an Azure blob storage external data source  
In this example, the data source is an Azure Storage Blob called *dailylogs* in the Azure account named *myaccount*. The WASB data source is for data transfer only; it does not support predicate pushdown.  
  
```  
CREATE EXTERNAL DATA SOURCE wasb1  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'wasbs://dailylogs@myaccount.blob.core.windows.net/'  
);  
```  
  
## See Also  
[PolyBase &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/polybase-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-external-table-sql-server-pdw.md)  
[CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-external-table-as-select-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
