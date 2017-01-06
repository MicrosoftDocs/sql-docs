---
title: "Loading Data into Hadoop (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0efe1330-eeac-4001-9d5b-579948b5f0ac
caps.latest.revision: 16
author: BarbKess
---
# Loading Data into Hadoop (Analytics Platform System)
When loading data into Hadoop, the Analytics Platform System HDInsight Region supports standard Hadoop methods such as using a map-only job for batch load, using the WebHDFS REST API for trickle-load, and using Sqoop. You can also load data using Polybase, and the HDInsight Developer Dashboard.  
  
> [!NOTE]  
> HDInsight running on Azure stores data using Azure blob storage (WASB). The HDInsight Region of Analytics Platform System stores data by using HDFS.  
  
## <a name="top"></a>HDInsight data loading scenarios  
Different scenarios call for different methods of loading data into the HDInsight cluster. In this section five scenarios are discussed:  
  
-   [Using a Hadoop map job for batch data load](#maponly)  
  
-   [Using trickle load via the WebHDFS REST API](#WebHDFS)  
  
-   [Using Sqoop for loading data from external relational sources](#Sqoop)  
  
-   [Using PolyBase, for archiving relational data](#PolyBase)  
  
-   [Using Developer Dashboard to upload files](#DevDash)  
  
> [!NOTE]  
> The examples in this topic present IP addresses with the first two octets masked as `xxx.xxx` and passwords masked as **********. Modify the examples with data that matches your environment.  
  
## <a name="maponly"></a>Using a Hadoop map job for batch data load  
Use a map-only Hadoop job when loading a large amount (a batch) of data from the corporate network. The data source can be any location accessible to the customer but most often this is a network share with multiple files containing data to be loaded at about the same time. A typical example is a job regularly scheduled to run at a predefined time (usually once a day), that will load, curate, and analyze all incremental data changes that occurred since the previous job run.  
  
### Network configuration  
The network share that contains the data for loading should be configured to be accessible from the HDI region HeadNode. How to enable this depends on network settings. Typical items to configure include making sure that file sharing is allowed, the firewall is not preventing file sharing (check outbound data access for HDInsight in DW Config), etc.  
  
To protect data transferred over the network, administrators should configure SMP 3.0 or to consider other mechanisms to ensure data privacy on their shares.  
  
After configuring a shared folder, the administrator should double-check that the settings are correct trying to access the data source by using windows explorer:  
  
![Access a network share using Windows Explorer](../hdinsight/media/APS_HDI_AccessNetworkShare.png "APS_HDI_AccessNetworkShare")  
  
Before getting access the user will be prompted for credentials. It is good practice to use credentials with low (read-only) privileges and limited validity period, as the credentials may be visible in the Hadoop logs if provided as job input parameters. Once the administrator makes sure the data is accessible and available, a map job can be configured for data loading.  
  
### Configuring and invoking a data loading job  
The following sections describe a typical example of a data loading job. **The method explained here (and the code provided) is not the only solution to this problem but it illustrates one possible approach.**  
  
The jar file with the code must be uploaded into HDFS.  
  
> [!IMPORTANT]  
> When you build the .jar file make sure to use Java for Windows that matches the version deployed on your APS appliance.  
  
All input files describing the load should be placed in HDFS. The following is the input file format for the provided code example:  
  
```  
SomeSharedFolder#SourceFileName1#HDFSDestinationPath#DestinationFilename1  
SomeSharedFolder#SourceFileName2#HDFSDestinationPath#DestinationFilename2  
…  
SomeSharedFolderY#SourceFileNameX#HDFSDestinationPathZ#DestinationFilenameX  
```  
  
Character **#** is used as delimiter which enables the map job to distinguish the following data elements:  
  
-   Name of the folder at source  
  
-   File name at source  
  
-   Destination folder in HDFS  
  
-   File name in HDFS  
  
Here is an example for the file content. Replace the IP address at the start of each line with an appropriate IP address in your environment:  
  
```  
\\xxx.xxx.255.79\CorpShare\FileShare\#FileFromShare001.txt#/sp/lfs/t001/data/#FileFromShare001.txt  
\\xxx.xxx.255.79\CorpShare\FileShare\#FileFromShare002.txt#/sp/lfs/t001/data/#FileFromShare002.txt  
\\xxx.xxx.255.79\CorpShare\FileShare\#FileShare01.txt#/sp/lfs/t001/data/#FileShare01.txt  
\\xxx.xxx.255.79\CorpShare\FileShare\#FileShare02.txt#/sp/lfs/t001/data/#FileShare02.txt  
\\xxx.xxx.255.79\CorpShare\FileShare\#FileShare03.txt#/sp/lfs/t001/data/#FileShare03.txt  
\\xxx.xxx.255.79\CorpShare\FileShare\#FileShare04.txt#/sp/lfs/t001/data/#FileShare04.txt  
\\xxx.xxx.255.53\Share3\Files\#file1.txt#/sp/lfs/t001/data/#File1.txt  
\\xxx.xxx.255.53\Share3\Files\#file2.txt#/sp/lfs/t001/data/#File2.txt  
\\xxx.xxx.255.53\Share3\Files\#file3.txt#/sp/lfs/t001/data/#File3.txt  
\\xxx.xxx.255.53\Share3\Files\#file4.txt#/sp/lfs/t001/data/#File4.txt  
```  
  
The next portion of this example shows how to invoke the load code. This example assumes that jar with map job is located in the HDFS path **/CustomCode/Load/LoadFromShare.jar**, and the input file is located at **/Data/Load/Load1/InputFile1.txt**. Note that the job expects named parameters for *input_path*, *output_path*, and *network_shares*. If you don't specify the parameter names correctly the job will end with an error message.  
  
You can invoke data loading job using one of the following options:  
  
-   Run **curl.exe** externally and invoke the Templeton (WebHCat) REST API.  
  
    Example command line for job invocation using curl and WebHCat:  
  
    ```  
    curl.exe "https://xxx.xxx.217.107/templeton/v1/mapreduce/jar" -s -d   
    user.name=Administrator -d jar="/CustomCode/Load/LoadFromShare.jar" -d   
    class=loadfromshare.LoadFromShare -d arg=input_path:/Data/Load/Load1/ -d   
    arg=output_path:/Data/Load/Load1Result/ -d arg="network_shares:  
     HYPERLINK "file:///\\\\xxx.xxx.255.79\\CorpShare\\FileShare,Password1,LocalTestUser%23" \\xxx.xxx.255.79\CorpShare\FileShare,Password1,LocalTestUser#  
    \\xxx.xxx.217.98\Share3\Files,Password2,CORPDOMAIN\DomainAdmin" -i -u   
    Administrator:********* –k  
    ```  
  
-   Run the hadoop **jar** command from the HDInsight head node.  
  
    Example command line for job invocation hadoop jar (assuming that loadfromshare.jar is located in the c:\LoadFromShare folder):  
  
    ```  
    c:\LoadFromShare>hadoop jar loadfromshare.jar   
    loadfromshare.LoadFromShare "input_path:/Data/Load/Load1/"   
    "output_path:/Data/Load/Load1Result/" "network_shares:   
    \\xxx.xxx.255.79\CorpShare\FileShare,Password1,LocalTestUser#  
    \\xxx.xxx.217.98\Share3\Files,Password2,CORPDOMAIN\DomainAdmin"  
    ```  
  
> [!NOTE]  
> The curl tool can be downloaded from [cURL Releases and Downloads](http://curl.haxx.se/download.html).  
  
The following table describes parameters used in the command above:  
  
|Parameter|Value Provided|Comment|  
|-------------|------------------|-----------|  
|Secure Node Gateway|xxx.xxx.217.107 (Replace with your IP address)|Address of the Secure Node is necessary when job is submitted through WebHCat (Templeton) API.|  
|User.name|Administrator|User which submits request. It should align with credentials provided in –u parameter.|  
|"input_path"|/Data/Load/Load1/|HDFS path that will be used as the map-reduce input. This path may contain one or more input files. In general there may also be many input paths but code provided here assumes there is only one.|  
|"output_path"|/Data/Load/Load1Result/|HDFS path that will be used as the map-reduce output. This path must not exist when job is started. If it exists, the job will fail.|  
|"network_shares"|LocalTestUser#\\\xxx.xxx.217.98\Share3\Files,Password2,CORPDOMAIN\DomainAdmin|Argument containing information on credential to access file shares: file share path, username and password.|  
|u|Administrator:********* (Replace with your administrator password.)|HTTP basic authentication credentials of the user who submits the WebHCat (Templeton) request.|  
  
### Data loading - code example  
The code that performs the load is a map-only job. That means that mappers will copy the data, and that reducer does not exists because there is nothing that should be done after copying. Reducer might be a good place for some custom code that creates a data load summary (total amount of loaded data, how much time data load took, etc.), but this is not covered by this example.  
  
This code example has two classes: `LoadFromShare` and `LoadFromShareMapper` which extends Mapper class.  
  
LoadFromShare  
In `LoadFromShare` class we setup the map-reduce job. There are a few specific for this setup. We use `NLineInputFormat`. This means that each mapper will process only `N` lines from the input file (there may be many input files). In the code below `N` equals 1. The rationale behind this, is that file copy is long operation and the overhead of starting map task for each file copy is small. If that is not true (when data loading deals with large number of small files), then the developer should consider increasing `N` from 1 to some greater value. Additionally, a third parameter is processed and values from it are added to job configuration. By doing this we enable each mapper to have information on shares and credentials for them.  
  
LoadFromShareMapper  
`LoadFromShareMapper` code in setup reads info on shares from the configuration and calls **NET USE** command to get rights to access the shares. Mapper as input (value parameter) gets one line of an input file, extracts from it source and destination for copy operation and then copies the file in chunks of the size of the buffer. When certain amount of data is copied mapper sends `progress` signal to the job tracker. This is done because copy might take a lot of time and long-running unresponsive tasks are killed by the JobTracker. By calling the `progress` method, mapper tells JobTracker that it is alive.  
  
> [!NOTE]  
> The code sample below is an example how job for data loading might look like. It’s is not designed to catch and surface every possible error that might happen in a real use case. You should adjust it to your specific environment. Keep in mind following issues:  
>   
> -   If your .jar file is corrupted (or compiled with the wrong JDK version), the error message returned in the case of WebHDFS submission won’t properly indicate root cause of the problem.  
> -   The **NET USE** command generated from java code may fail silently for some reason (there is a comment on this in the code). You should check whether a   manually submitted **NET USE** command works properly in your environment.  
  
```  
package loadfromshare;  
  
import org.apache.hadoop.fs.Path;  
import org.apache.hadoop.io.IntWritable;  
import org.apache.hadoop.io.Text;  
import org.apache.hadoop.mapreduce.Job;  
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;  
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;  
import org.apache.hadoop.mapreduce.lib.input.NLineInputFormat;  
  
/**  
*  
* @author  
*/  
public class LoadFromShare {  
  
    /**  
     * @param args the command line arguments  
     */  
    public static void main(String[] args) throws Exception {  
  
        String inputPathParam = null;  
        String outputPathParam = null;  
        String networkSharesParam = null;  
  
        String inputPathParamPrefix = "input_path:";  
        String outputPathParamPrefix = "output_path:";  
        String networkSharesParamPrefix = "network_shares:";  
  
        for(int i = 0; i < args.length; i++)  
        {  
            System.out.println("args[" + i + "] " + args[i]);  
            if (args[i].indexOf(inputPathParamPrefix) == 0)  
            {  
                inputPathParam = args[i].substring(inputPathParamPrefix.length(), args[i].length());  
            }  
            else if (args[i].indexOf(outputPathParamPrefix) == 0)  
            {  
                outputPathParam = args[i].substring(outputPathParamPrefix.length(), args[i].length());  
            }  
            else if (args[i].indexOf(networkSharesParamPrefix) == 0)  
            {  
                networkSharesParam = args[i].substring(networkSharesParamPrefix.length(), args[i].length());  
            }  
        }  
  
        if (inputPathParam == null || outputPathParam == null || networkSharesParam == null)  
        {  
            throw new Exception("Input parameters are invalid. You must specify 'input_path:', 'output_path:' and  'network_shares:' .");  
        }  
        System.out.println("inputPathParam: " + inputPathParam);  
        System.out.println("outputPathParam: " + outputPathParam);  
        System.out.println("networkSharesParam: " + networkSharesParam);  
  
        Job job = new Job();  
        job.setJarByClass(LoadFromShare.class);  
        job.setJobName("LoadFromShare");  
       job.setMapperClass(LoadFromShareMapper.class);  
        job.setOutputKeyClass(Text.class);  
        job.setOutputValueClass(IntWritable.class);  
  
        // When NLineInputFormat is used, a map task is started  
        // for each N lines in the input file.  
        // Without this, map task would be started   
        // for each file in the input.  
        //  
        job.setInputFormatClass(NLineInputFormat.class);  
  
        // If files that should be copied are large, this setting (value == 1) is good.  
        // If files are small, consider increasing number of lines per split.  
        // This would decrease overhead of starting map task.  
        //  
        NLineInputFormat.setNumLinesPerSplit(job, 1);  
  
        FileInputFormat.addInputPath(job, new Path(inputPathParam));  
        FileOutputFormat.setOutputPath(job, new Path(outputPathParam));  
  
        String [] networkSharesInfo = networkSharesParam.split("#");  
        Integer numberOfShares = networkSharesInfo.length;  
        for(int i = 0; i < networkSharesInfo.length; i++)  
        {  
            String info = networkSharesInfo[i];  
            System.out.println("--Share_" + i + " ; " + info);  
            job.getConfiguration().set("Share_" + i, info);  
        }  
        System.out.println("--NumberOfShares" + " ; " + numberOfShares.toString());  
        job.getConfiguration().set("NumberOfShares", numberOfShares.toString());  
        job.getConfiguration().setInt("mapred.reduce.tasks", 0);  
  
        System.exit(job.waitForCompletion(true) ? 0 : 1);  
    }  
}  
  
package loadfromshare;  
  
import java.io.IOException;  
import org.apache.hadoop.io.IntWritable;  
import org.apache.hadoop.io.LongWritable;  
import org.apache.hadoop.io.Text;  
import org.apache.hadoop.mapreduce.Mapper;  
import java.io.*;  
import org.apache.hadoop.fs.*;  
import org.apache.hadoop.conf.*;  
  
/**  
*  
* @author  
*/  
import org.apache.hadoop.fs.Path;  
import org.apache.hadoop.io.IntWritable;  
import org.apache.hadoop.io.Text;  
  
public class LoadFromShareMapper extends Mapper<LongWritable, Text, Text, IntWritable> {  
  
    public int ioBufferSizeMB = 16;  
  
    @Override   
    public void setup(Context context)  
            throws IOException  
    {          
        int numberOfShares = Integer.parseInt(context.getConfiguration().get("NumberOfShares"));  
        System.out.println(">NumberOfShares: " + numberOfShares);  
        System.out.println(">Share_0: " + context.getConfiguration().get("Share_0"));  
        for(Integer i = 0; i < numberOfShares; i++)  
        {  
            String key = "Share_" + i.toString();  
            System.out.println(">key: " + key);  
            String share = context.getConfiguration().get(key);  
            System.out.println(">share: " + share);  
            String[] tokens = share.split(",");  
            String netuseCommand = "cmd /c net use " + tokens[0] + " " + tokens[1] + " /USER:" + tokens[2] + " /persistent:yes";  
            System.out.println(">cmd: " + netuseCommand);  
            Process process = Runtime.getRuntime().exec(netuseCommand);  
            try   
            {  
                // Note that output of NET USE is not verified in this example.  
                // If for any reason this command fails, that will be silent at this point,  
                // but may cause failure in later file copy operations.  
                //  
                process.waitFor();  
            }   
            catch (InterruptedException e)  
            {  
                System.out.println("InterruptedException occured");  
                System.out.println(e.toString());  
            }  
            OutputStream os = process.getOutputStream();  
            PrintStream ps = new PrintStream(os);  
            ps.println();  
        }  
    }  
  
    @Override  
    public void map(LongWritable key, Text value, Context context)  
            throws IOException, InterruptedException {  
  
        String[] params = value.toString().split("#");  
        String sharedFolder = params[0];  
        String sharedFile = params[1];  
        String hdfsPath = params[2];  
        String hdfsFile = params[3];  
  
        FileSystem hdfs = FileSystem.get(new Configuration());  
        System.out.println("Creating outFile: " + hdfsPath + hdfsFile);  
        FSDataOutputStream outFile = hdfs.create(new Path(hdfsPath + hdfsFile));  
        System.out.println("Creating inFile: " + sharedFolder + sharedFile);  
  
        try {  
            FileInputStream in = new FileInputStream(sharedFolder + sharedFile);  
  
            System.out.println("Done creting files.");  
  
            long progressSize = 64 * 1024 * 1024l;  
            long copiedSize = 0;  
            byte[] buf = new byte[ioBufferSizeMB * 1024 * 1024];  
            int len = 0;  
  
            while ((len = in.read(buf)) > 0) {  
                outFile.write(buf, 0, len);  
                copiedSize += len;  
  
                // Copying data may be a long running operation.  
                // Each time size of copied data reaches a certain   
                // (relatively small) limit mapper sends progress signal.  
                // This prevents job tracker to kill long running mapper.  
                //  
                if (copiedSize >= progressSize) {  
                    copiedSize = 0;  
  
                    // Tell job tracker that this mapper is alive and   
                    // should not be killed, even though it is long running.  
                    //  
                    context.progress();  
                }  
            }  
  
            in.close();  
            outFile.close();  
        }  
        catch (Exception e)  
        {  
            System.out.println(e.toString());  
        }  
    }  
}  
```  
  
**Compiling the code**  
  
Follow the next steps to correctly compile java code:  
  
1.  Create a folder **c:\loadfromshare** on the HDInsight  HeadNode and copy the two **.java** files (**LoadFromShareMapper.java** and **LoadFromShare.java**) to that folder.  
  
2.  Run two commands to compile **.java** files in the following order:  
  
    ```  
    c:\LoadFromShare>C:\Java\jdk1.7.0_51\bin\javac -classpath   
    C:\hadoop\hadoop-2.2.0.2.0.8.0-1639\share\hadoop\mapreduce\  
    hadoop-mapreduce-client-core-2.2.0.2.0.8.0-1639.jar;  
    C:\hadoop\hadoop-2.2.0.2.0.8.0-1639\share\hadoop\common\  
    hadoop-common-2.2.0.2.0.8.0-1639.jar -d C:\loadfromshare\   
    C:\loadfromshare\LoadFromShareMapper.java  
    ```  
  
    ```  
    c:\LoadFromShare>C:\Java\jdk1.7.0_51\bin\javac -classpath   
    C:\hadoop\hadoop-2.2.0.2.0.8.0-1639\share\hadoop\mapreduce\  
    hadoop-mapreduce-client-core-2.2.0.2.0.8.0-1639.jar;  
    C:\hadoop\hadoop-2.2.0.2.0.8.0-1639\share\hadoop\common\  
    hadoop-common-2.2.0.2.0.8.0-1639.jar;;c:\loadfromshare   
    -d C:\loadfromshare\ C:\loadfromshare\LoadFromShare.java  
    ```  
  
3.  Use the jar command to package the class files from the root folder (in this case:  **c:\loadfromshare**):  
  
    ```  
    c:\LoadFromShare>C:\Java\jdk1.7.0_51\bin\jar cvf   
    loadfromshare.jar .\loadfromshare\*.class  
    ```  
  
[HDInsight data loading scenarios](#top)  
  
## <a name="WebHDFS"></a>Using trickle load via the WebHDFS REST API  
This method of loading is convenient to store data that is generated by multiple application instances (data sources) where input files are usually relatively small (compared to batch load) but loading frequency is considerably high and data loading is performed in a concurrent, parallel fashion. Typical examples include data continuously fed by sensors, power meters, or applications that continuously append incremental changes to their log files. In this scenario, the best approach is to load information by using an application that uses the WebHDFS API.  
  
The following example illustrates how this API is used from one common HTTP tool – curl. Loading data with WebHDFS is two-step operation. In the first step, the client application specifies file path (including the file name) where data will be stored. Similar to WebHCat, the `user.name` specifies identity of the user and the `–u` parameter is used for HTTP basic authentication:  
  
```  
curl -i -X PUT "https://xxx.xxx.217.107/webhdfs/v1/trickleload/file3.txt?user.name=HDIUser&op=CREATE" -k -u HDIUser:Password1 -H Content-Length:0  
Received response is a temporary redirect:  
HTTP/1.1 307 TEMPORARY_REDIRECT  
Content-Length: 0  
Content-Type: application/octet-stream  
Expires: Thu, 01-Jan-1970 00:00:00 GMT  
Location: https://xxx.xxx.217.107/rwebhdfs/H16021-C-HDN002/50075/webhdfs/v1/trickleload/file3.txt?op=CREATE&user.name=Administrator&overwrite=false  
Server: Microsoft-IIS/8.0  
Set-Cookie: hadoop.auth="u=Administrator&p=Administrator&t=simple&e=1386738798008&s=xJtW0Fc8s484kofGte2oQuH/iJs=";Path=/  
X-Powered-By: ARR/2.5  
X-Powered-By: ASP.NET  
Date: Tue, 10 Dec 2013 19:13:18 GMT  
```  
  
The received location should be used for next request. Along with the redirect location, the second request must contain a local file path, user.name, and credentials for HTTP basic authentication:  
  
```  
curl -i -X PUT -T c:\tmp\file3.txt "https://xxx.xxx.217.107/  
rwebhdfs/H16021-C-HDN002/50075/webhdfs/v1/trickleload/file3.txt?op=  
CREATE&user.name=Administrator&overwrite=false" -k   
-u Administrator:*********  
```  
  
The final response contains the status of the entire operation:  
  
```  
HTTP/1.1 201 Created  
Content-Length: 0  
Content-Type: application/octet-stream  
Location: webhdfs://xxx.xxx.217.107:443/trickleload/file3.txt  
Server: Microsoft-IIS/8.0  
X-Powered-By: ARR/2.5  
X-Powered-By: ASP.NET  
Date: Tue, 10 Dec 2013 19:14:28 GMT  
```  
  
WebHDFS can be used for other file manipulation, including reading existing files, folder creation and renaming, file deletion, etc. The full WebHDFS reference can be found at [WebHDFS REST API](http://hadoop.apache.org/docs/r1.0.4/webhdfs.html)  
  
[HDInsight data loading scenarios](#top)  
  
## <a name="Sqoop"></a>Using Sqoop for loading data from external relational sources  
Use Sqoop to transfer data from relational data sources into the HDInsight for analysis. (Use [Using PolyBase, for archiving relational data](#PolyBase) when the source of the data is SQL Server PDW.) To execute the Sqoop command, the administrator usually needs to use Sqoop command line tool located on the headnode. This requires a remote connection to the cluster. Often, Sqoop is part of complex data loading workflow, where it can be one of several workflow actions. In that scenario, another REST API (Oozie) is used to define and run the data loading workflow. Since Oozie is exposed externally (similarly to other REST APIs), this approach doesn’t require a remote connection to the head node which means that regular HDInsight users (without cluster admin permissions) can invoke Oozie and Sqoop.  
  
The following example illustrates using single theSqoop command from the command line tool. First the user connects remotely to the headnode. Then the user opens the command line tool and goes to directory **C:\hadoop\sqoop-1.4.3.1.3.0.0-0539**. (The HDP version number may be different on your appliance.)  
  
The following example loads data using Sqoop from the command line:  
  
```  
sqoop import --driver com.microsoft.sqlserver.jdbc.SQLServerDriver   
--connect "jdbc:sqlserver://xxx.xxx.30.40:1433;Database=DatabaseName"   
--table TableName --username User001 -P --fields-terminated-by \t –m 1  
 –target-dir /user/MyUserName/DatabaseName/TableName/  
```  
  
In this example, `xxx.xxx.30.40:1433` is the IP address and TCP port number of the instance of SQL Server that contains the data.  
  
In order to use Sqoop, outbound connections must be enabled by a firewall rule in DW Config. HDInsight only supports SQL Server Authentication for Sqoop commands.  
  
The example loads the entire `TableName` table from `DatabaseName` database into the HDFS path **/user/MyUserName/DatabaseName/TableName/**. Sqoop has many more options and variations of how data load can be done.  
  
[HDInsight data loading scenarios](#top)  
  
## <a name="PolyBase"></a>Using PolyBase, for archiving relational data  
Use PolyBase to move older or less relevant data out of SQL Server PDW into HDInsight to reduce the price of storing data. This *cold data store* scenario archives older data in a way that the data still can be queried in a truly unified way along with the current relational data that is stored in PDW.  
  
The mechanism which enables this scenario (archiving and unified querying) is PolyBase, a specific PDW feature. It enables users to create external tables in HDInsight and then use those tables as any other relational table (and in conjunction with relational table). Although external tables can be created on existing HDFS files, in this scenario we are particularly interested in external tables that are created and loaded based on data that is stored in PDW tables.  
  
The LOCATION must specify the HeadNode for your HDInsight workload. The following example loads data from the `AdventureWorksPDW2012.dbo.dimCustomer` table into HDFS on the path **/database/HDI_dimCustomer**. *<HDInsight Domain Name>***-C-HHN01** is the name of the HeadNode on the internal InfiniBand network.  
  
```  
CREATE EXTERNAL DATA SOURCE HDI_source  
WITH (  
    TYPE = HADOOP,  
    LOCATION = 'hdfs://<HDInsight Domain Name>-C-HHN01:8020'  
)  
  
CREATE EXTERNAL FILE FORMAT HDI_format  
WITH (  
    FORMAT_TYPE = DELIMITEDTEXT,   
    FORMAT_OPTIONS (FIELD_TERMINATOR ='|')  
);  
  
CREATE EXTERNAL TABLE HDI_Table   
WITH (  
        LOCATION='/database/HDI_Table',  
        DATA_SOURCE = HDI_source,  
        FILE_FORMAT = HDI_format  
    )  
AS SELECT * FROM AdventureWorksPDW2012.dbo.dimCustomer  
;  
```  
  
This command also creates an external table in PDW with the name `HDI_dimCustomer`. After execution of this command, the `HDI_dimCustomer` file in HDFS will be populated with data from `AdventureWorksPDW2012.dbo.dimCustomer`. Once the external table is created it can be used in PDW DSQL queries as if it was created directly upon HDFS data.  
  
PolyBase queries always access the HDInsight region directly (bypassing the Secure Node Gateway) by specifying the IP addresses or names from the internal InfiniBand network. No authentication is required for this type of request, regardless if data is transferred from HDInsight to PDW or the other way around (as in this example). For more information, see [PolyBase &#40;SQL Server PDW&#41;](../sqlpdw/polybase-sql-server-pdw.md).  
  
[HDInsight data loading scenarios](#top)  
  
## <a name="DevDash"></a>Using Developer Dashboard to upload files  
Use the HDInsight Developer Dashboard for loading smaller utility files that support execution of MapReduce jobs or Oozie workflows, submitted and executed through REST API channels. Data loading in this scenario is limited in several ways: it’s manual, covering only files from client local file system, and supporting a size equal or less to 20 MB. However, this scenario, has its place as a file upload utility that facilitates job submission scenarios. For more information about uploading files with Developer Dashboard, see [Upload File - HDInsight Region Developer Dashboard &#40;Analytics Platform System&#41;](../hdinsight/upload-file-hdinsight-region-developer-dashboard-analytics-platform-system.md).  
  
[HDInsight data loading scenarios](#top)  
  
## See Also  
[Using Hadoop Tools with APS &#40;Analytics Platform System&#41;](../hdinsight/using-hadoop-tools-with-aps-analytics-platform-system.md)  
[Tips for Using the REST API &#40;Analytics Platform System&#41;](../hdinsight/tips-for-using-the-rest-api-analytics-platform-system.md)  
  
