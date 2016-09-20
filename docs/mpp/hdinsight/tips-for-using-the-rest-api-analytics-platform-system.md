---
title: "Tips for Using the REST API (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2bd682b4-caa4-4fb7-94fd-e0831af6e79a
caps.latest.revision: 6
author: BarbKess
---
# Tips for Using the REST API (Analytics Platform System)
The HDInsight region of Analytics Platform System supports access from tools and applications using the Hadoop REST API's.  
  
## <a name="AllAPI"></a>All API's  
Important notes for all APIs:  
  
-   Only HTTPS is enabled  
  
-   No port number is required in the URI  
  
-   Specification of user identity in the request is mandatory (anonymous requests are not allowed)  
  
    -   The Secure Node Gateway performs authentication on behalf of the end points  
  
    -   The end points themselves are not performing additional authentication but are accepting the user identity as a `tag` for requests and transferring the identity correctly into the Hadoop cluster (i.e. ownership of files/folders, jobs, etc.).  
  
    -   For additional information about the HDInsight security model, see [Understanding the Security Model of the HDInsight Region &#40;Analytics Platform System&#41;](../../mpp/hdinsight/understanding-the-security-model-of-the-hdinsight-region-analytics-platform-system.md).  
  
-   `PUT` requests must contain a specified content length. A length of 0 can be provided. This is a requirement imposed by the Secure Node Gateway.  
  
The following examples for accessing Rest API use curl command line tool, a commonly used REST API client. Any REST API client supporting HTTP basic authentication can be used. In addition, users can submit requests from custom applications (.NET or other).  
  
> [!NOTE]  
> The curl tool can be downloaded from [cURL Releases and Downloads](http://curl.haxx.se/download.html).  
  
## <a name="WebHDFS"></a>Remotely accessing HDFS (WebHDFS)  
HDInsight exposes the WebHDFS REST API end point through the Secure Node Gateway. The API is used for HDFS file folder browsing and manipulation. The Gateway acts as a proxy for all requests sent to the cluster.  
  
```  
--List status of the user’s home directory (replace <username> and <password> with your account data)  
curl -i -k -u <username>:<password> "https://<secure_node_IP>/webhdfs/v1/user/<username>?user.name=<username>&op=LISTSTATUS"  
  
--Create file in user’s home directory: request 1  
curl -i -H "Content-Length:0" -k -u <username>:<password> -X PUT "https://<secure_node_IP>/webhdfs/v1/user/<username>/hellowebhdfs.txt?user.name=<username>&op=CREATE"  
  
--Response (part)  
HTTP/1.1 307 TEMPORARY_REDIRECT  
Content-Length: 0  
Content-Type: application/octet-stream  
Expires: Thu, 01-Jan-1970 00:00:00 GMT  
Location: https:// <secure_node_IP>/rwebhdfs/<datanode_name>/50075/webhdfs/v1/user/<username>/hellowebhdfs.txt?op=CREATE&user.name=<username>&overwrite=false  
  
--Upload file content (redirect): request 2   
--(can be automatically handled by curl if –L option was used in initial request)  
curl -i -k -u <username>:<password> -X PUT -T "C:\tests\hellowebhdfs.txt" "https:// <secure_node_IP>/rwebhdfs/<datanode_name>/50075/webhdfs/v1/user/<username>/hellowebhdfs.txt?op=CREATE&user.name=<username>&overwrite=false"  
```  
  
## <a name="WebHCAT"></a>Remote Job Submission (WebHCat also known as Templeton)  
HDInsight exposes the WebHCat REST API end point through the Secure Node Gateway. The API is used for job submission, monitoring, and management. The Gateway acts as a proxy for all requests sent to the cluster.  
  
```  
--Check the status of the WebHCat service  
curl -k -u <username>:<password> "https://<secure_node_IP>/templeton/v1/status?user.name=<username>"  
  
--Response (if all OK)  
{"status":"ok","version":"v1"}  
  
--Submit new job (Hive query)  
curl -i -k -u <username>:<password> -d execute="show tables;" –d statusdir="/user/<username>/hive/show_tables_output" "https://<secure_node_IP>/templeton/v1/hive?user.name=<username>"   
  
--Response (part)  
{"id":"job_201312102153_0011"}  
  
--Get job status (for a given job_id)  
curl -k -u <username>:<password> "https://<secure_node_IP>/templeton/v1/jobs/job_201312102153_0011?user.name=<username>"  
  
--Cancel executing job  
curl -X DELETE  -k -u <username>:<password> "https://<secure_node_IP>/templeton/v1/jobs/ job_201312102153_0011? user.name=<username>"  
```  
  
## <a name="Oozie"></a>Remote Workflow Submission and Scheduling (Oozie)  
HDInsight exposes the Oozie REST API end point through the Secure Node Gateway. The API is used for workflow and job submission, scheduling and monitoring, and management. Workflows usually consist of multiple jobs and serve the purpose of end-to-end operationalization. The Secure Node Gateway acts as a proxy for all requests sent to the cluster.  
  
Specific notes for this API: Specification of the user identity is not done as part of URL but rather through the `user.name` property in the XML content of the request. This is demonstrated in the POST example below.  
  
```  
--Check the status of the Oozie service / get version  
curl –i -k -u <username>:<password> "https://<secure_node_IP>/oozie/versions "  
  
--Response (if all OK)  
HTTP/1.1 200 OK  
Content-Length: 5  
Content-Type: application/json;charset=UTF-8  
…  
[0,1]  
  
--Submit new workflow  
curl -k -X POST -H "Content-Type: application/xml" -u <username>:<password> "https://<secure_node_IP>/oozie/v1/jobs" -d @<local_path>/oozie-example.xml  
where oozie-example.xml is something like:  
  
<?xml version="1.0" encoding="UTF-8"?>  
<configuration>  
<property>  
<name>user.name</name>  
<value><username></value>  
</property>  
<property>  
<name>oozie.wf.application.path</name>  
<value>hdfs:///<hdfs-path-to-workflow-definition></value>  
</property>  
</configuration>  
  
--Response (part)  
{"id":"0000000-131210215336175-oozie-Hado-W"}  
  
--Start the workflow execution  
curl -i -k -X PUT -H "Content-Length:0" -u <username>:<password> "https://<secure_node_IP>/oozie/v1/job/0000000-131210215336175-oozie-Hado-W?action=start"  
  
--Inquire the job status  
curl -i -k -u <username>:<password> "https://<secure_node_IP>/oozie/v1/job/0000000-131210215336175-oozie-Hado-W?show=info"  
```  
  
## See Also  
[Loading Data into Hadoop &#40;Analytics Platform System&#41;](../../mpp/hdinsight/loading-data-into-hadoop-analytics-platform-system.md)  
[Using Hadoop Tools with APS &#40;Analytics Platform System&#41;](../../mpp/hdinsight/using-hadoop-tools-with-aps-analytics-platform-system.md)  
  
