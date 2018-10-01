---
title: How to use notebooks in SQL Server 2019 CTP 2.0 | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---

# How to use notebooks in SQL Server 2019 CTP 2.0

This how you can launch notebooks on the cluster and start authoring your own Notebooks. It also shows how to submit jobs against the cluster.

## Connect to the Hadoop Gateway Knox end point

You can connect to different end-points in the cluster. You can connect to the Microsoft SQL Server connection type or to the HDFS/Spark Gateway end-point.

In Azure Data Studio (preview), type **F1** > **New Connection**and you can connect to your HDFS/Spark Gateway end-point.

![image1](media/notebooks-guidance/image1.png)

## Browse HDFS
Once you connect you will be able to browse your HDFS folder. WebHDFS is started when the deployment is completed, and you will be able to **Refresh**, add **New Directory**,**Upload** files and **Delete**.

![image2](media/notebooks-guidance/image2.png)

These simple operations let you bring your own data into HDFS.

## Launch new Notebooks

You can launch a new notebook multiple ways:

1. From the Manage Dashboard. On making a new connection you will see a Dashboard. Click on New Notebook task from the Dashboard.

  ![image3](media/notebooks-guidance/image3.png)

1. Right click on the HDFS/Spark connection and you have New Notebook in the context menu

![image4](media/notebooks-guidance/image4.png)

Please provide a name of your Notebook (Example: *Test.ipynb*) and click **Save**.

![image5](media/notebooks-guidance/image5.png)

## Supported kernels and attach to context

In our Notebook Installation we support the PySpark and Spark, Spark Magic kernels which would allow users to write Python and Scala code using Spark. We also allow users to choose Python for their local development purposes.

![image6](media/notebooks-guidance/image6.png)

When you select one of these kernels we will install that kernel in the virtual environment and you can start writing code in the supported language.

| Kernel | Description
|---- |----
|PySpark Kernel| For writing Python code using Spark compute from the cluster.
|Spark Kernel|For writing Scala code using Spark compute from the cluster.
|Python Kernel|For writing Python code for local development.

The Attach to provides the context for the Kernel to attach. When you are connected to the HDFS/Spark Gateway (Knox) end-point the default Attach to will be that end-point of the cluster.

![image7](media/notebooks-guidance/image7.png)

## Hello world in the different contexts

### Pyspark kernel

Choose the PySpark Kernel and in the cell type in the following code:

![image8](media/notebooks-guidance/image8.png)

Click Run and you should see the Spark Application being started and you will see the following output:

![image9](media/notebooks-guidance/image9.png)

The output should look something similar to the following image.

![image10](media/notebooks-guidance/image10.png)

### Spark kernel
Add a new code cell by clicking the +Code command in the toolbar.

![image11](media/notebooks-guidance/image11.png)

Choose the Spark Kernel in the drop down for the kernels and in the cell type/paste in 

![image12](media/notebooks-guidance/image12.png)

Click **Run**  you should see the Spark Application being started and this will create the Sparksession  **spark** and will define the **HelloWorld** object.

The Notebook should look similar to the following image.

![image13](media/notebooks-guidance/image13.png)

Once you define the object then in the next Notebook cell type in the following code:

![image14](media/notebooks-guidance/image14.png)

Click **Run** in the Notebook menu and you should see the "Hello, world!" in the output.

![image15](media/notebooks-guidance/image15.png)

### Local python kernel
Choose the local Python Kernel and in the cell type in **

![image16](media/notebooks-guidance/image16.png)

This should simply output

![image17](media/notebooks-guidance/image17.png)

### Markdown Text
Add a new text cell by clicking the +Text command in the toolbar.

![image18](media/notebooks-guidance/image18.png)

Click on the preview icon to add your markdown

![image19](media/notebooks-guidance/image19.png)

Click on the preview icon again to toggle to see just the markdown

![image20](media/notebooks-guidance/image20.png)

## Manage Packages
One of the things we optimized for local Python development was to include the ability to install packages which customers would need for their scenarios. By default, we include the common packages like pandas, numpy etc., but if you are expecting a package which is not included then write the following code in the Notebook cell

```python
import <package-name>
```

Run this command. You will be getting a Module not Found error. If your package exists, then you will not get this error.

If you find a `Module not Found` error, then please click on the **Manage Packages** and this will launch the terminal with the path for your Virtualenv identified and will let you install packages locally. Please use the following command to install the packages **

```
./pip install <package-name>
```

Once the package is installed, you should be able to go in the Notebook cell and type in

```python
import <package-name>
```

and Run the cell and you should no longer get the Module not found error.
If you like to uninstall a package, then please use the following command from your terminal **

```
./pip uninstall <package-name>
```

## Next steps

For more information about SQL Server Big Data Clusters, see [What is SQL Server 2019 Big Data Clusters?](big-data-cluster-overview.md).
