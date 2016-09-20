---
title: "Using Hadoop Tools with APS (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3c61c51d-9826-4d38-ae11-62513c638604
caps.latest.revision: 11
author: BarbKess
---
# Using Hadoop Tools with APS (Analytics Platform System)
Standard Hadoop applications, tools, and techniques can be used with the MicrosoftAnalytics Platform System. In most cases, there are no behavior changes.  
  
## Supported Applications  
Analytics Platform System includes an instance of Hortonworks Data Platform installation and supports the following Hadoop components.  
  
|Component|Version for APS AU1|Version for APS AU2|  
|-------------|-----------------------|-----------------------|  
|Hadoop Core (HDFS, YARN, MapReduce)|1.2.0|2.2.0|  
|Hive|0.11.0|0.12.0|  
|HCatalog|0.11.0|0.12.0|  
|Pig|0.11.0|0.12.0|  
|Oozie|3.3.2|4.0.0|  
|Sqoop|1.4.3|1.4.4|  
  
For more information about each component version, see the Hadoop documentation at [Welcome to Apache™ Hadoop®!](http://hadoop.apache.org/)  
  
> [!NOTE]  
> HeadNode is the virtual machine which hosts several HDInsight (Hadoop) master services: NameNode, Resource Manager, secondary NameNode, HiveMetaStore, HistoryServer, HiveServer2, OozieServer, and WebHcatServer (templeton).  
  
## Special Considerations  
The following applications require special techniques.  
  
### Hive  
If you are using the [Microsoft Hive ODBC Driver](http://www.microsoft.com/en-us/download/details.aspx?id=40886) to connect to HDInsight from BI, analytics, and reporting tools, a trusted certificate is required.  
  
### Oozie  
To submit Oozie jobs use the command line tools installed on the Head node or submit a workflow remotely through Oozie REST API.  
  
When creating Oozie jobs, the action configuration block of the **workflow.xml** file, must specify a separate queue named **joblauncher**, for the Oozie job launcher. The **joblauncher** queue has the capacity of 25% of the total cluster capacity. This queue is only for **map only** control tasks. To direct Oozie controller tasks to this queue, add `oozie.launcher.mapred.job.queue.name=joblauncher` to the action configuration block in the **workflow.xml** file.  
  
For more information about Oozie, see [Chapter 4. Using HDP for Workflow and Scheduling (Oozie)](http://docs.hortonworks.com/HDPDocuments/HDP1/HDP-1.3.2/bk_dataintegration/content/ch_using-oozie.html).  
  
## Using the Hadoop REST API's  
Developers can submit requests by using the standard Hadoop REST APIs such as WebHDFS, WebHCat, and the Oozie Web Services API. Requests should be directed to the IP address of the HSN node (gateway). A TCP port number is not necessary. Provide basic authentication credentials (username, password) in the request header.  
  
## Additional References  
For more information about **REST** APIs, see [WebHDFS REST API](http://hadoop.apache.org/docs/r1.0.4/webhdfs.html), [Chapter 1. Using WebHDFS REST API](http://docs.hortonworks.com/HDPDocuments/HDP1/HDP-1.3.2/bk_webhdfs/content/ch_webhdfs.html) and [2. Using WebHCat](http://docs.hortonworks.com/HDPDocuments/HDP1/HDP-1.3.2/bk_dataintegration/content/ch_using_hcatalog_1.html)  
  
## See Also  
[Loading Data into Hadoop &#40;Analytics Platform System&#41;](../../mpp/hdinsight/loading-data-into-hadoop-analytics-platform-system.md)  
  
