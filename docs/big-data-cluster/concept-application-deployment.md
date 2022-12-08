---
title: Introducing application deployment in SQL Server Big Data Clusters
titleSuffix: SQL Server Big Data Clusters
description: Learn how application deployment provides interfaces to create, manage, and run applications on a SQL Server 2019 Big Data Cluster.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 04/12/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
dev_langs:
  - "yaml"
  - "console"
ms.metadata: seo-lt-2019
---

# Introducing app deployment in [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Application deployment enables the deployment of applications on a [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)] by providing interfaces to create, manage, and run applications. Applications deployed on a Big Data Cluster benefit from the computational power of the cluster and can access the data that is available on the cluster. This increases scalability and performance of the applications, while managing the applications where the data lives. The supported application runtimes on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] are: R, Python, dtexec, and MLeap.

The following sections describe the architecture and functionality of application deployment.

## Application deployment architecture

Application deployment consists of a controller and app runtime handlers. When creating an application, a specification file (`spec.yaml`) is provided. This `spec.yaml` file contains everything the controller needs to know to successfully deploy the application. The following is a sample of the contents for `spec.yaml`:

```yaml
#spec.yaml
name: add-app #name of your python script
version: v1  #version of the app
runtime: Python #the language this app uses (R or Python)
src: ./add.py #full path to the location of the app
entrypoint: add #the function that will be called upon execution
replicas: 1  #number of replicas needed
poolsize: 1  #the pool size that you need your app to scale
inputs:  #input parameters that the app expects and the type
  x: int
  y: int
output: #output parameter the app expects and the type
  result: int
```

The controller inspects the `runtime` specified in the `spec.yaml` file and calls the corresponding runtime handler. The runtime handler creates the application. First, a Kubernetes ReplicaSet is created containing one or more pods, each of which contains the application to be deployed. The number of pods is defined by the `replicas` parameter set in the `spec.yaml` file for the application. Each pod can have one of more pools. The number of pools is defined by the `poolsize` parameter set in the `spec.yaml` file.

These settings determine the amount of requests the deployment can handle in parallel. The maximum number of requests at one given time is equals to `replicas` times `poolsize`. If you have 5 replicas and 2 pools per replica the deployment can handle 10 requests in parallel. See the image below for a graphical representation of `replicas` and `poolsize`:

![Poolsize and replicas](media/big-data-cluster-create-apps/poolsize-vs-replicas.png)

After the ReplicaSet has been created and the pods have started, a cron job is created if a `schedule` was set in the `spec.yaml` file. Finally, a Kubernetes Service is created that can be used to manage and run the application (see below).

When an application is executed, the Kubernetes service for the application proxies the requests to a replica and returns the results.

## <a id="app-deploy-security"></a> Security considerations for applications deployments on OpenShift

SQL Server 2019 CU5 enables support for BDC deployment on Red Hat OpenShift and an updated security model for BDC so privileged containers no longer required. In addition to non-privileged, containers are running as non-root user by default for all new deployments using [SQL Server 2019 CU5](release-notes-cumulative-updates-history.md#cu5).

At the time of the CU5 release, the setup step of the applications deployed with [app deploy](app-create.md) interfaces will still run as *root* user. This is required since during setup extra packages that application will use are installed. Other user code deployed as part of the application will run as low privilege user. 

In addition, `CAP_AUDIT_WRITE` capability is an optional capability necessary to allow scheduling SQL Server Integration Services (SSIS) applications using cron jobs. When the application's yaml specification file specifies a schedule, the application will be triggered via a cron job, which requires the extra capability. Alternatively, the application can be triggered on demand with `azdata app run` through a web service call, which does not require the `CAP_AUDIT_WRITE` capability. Note that `CAP_AUDIT_WRITE` capability no longer needed for `cronjob` starting from SQL Server 2019 CU8 release. 



> [!NOTE]
> The custom SCC in the [OpenShift deployment article](deploy-openshift.md) does not include this capability since it is not required by a default deployment of BDC. To enable this capability, you must first update the custom SCC yaml file to include CAP_AUDIT_WRITE.

```yaml
...
allowedCapabilities:
- SETUID
- SETGID
- CHOWN
- SYS_PTRACE
- AUDIT_WRITE
...
```

## How to work with app deploy inside Big Data Cluster

The two main interfaces for application deployment are: 

- [Command line interface [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]](app-create.md)
- [Visual Studio Code and Azure Data Studio extension](app-deployment-extension.md)

It is also possible for an application to be executed using a RESTful web service. For more information, see [Consume applications on Big Data Clusters](app-consume.md).

## App deploy scenarios

Application deployment enables the deployment of applications on a SQL Server BDC by providing interfaces to create, manage, and run applications.

:::image type="content" source="media/concept-application-deployment/big-data-cluster-app-pool-process-overview.png" alt-text="Identify sources (R, Python, SSIS (dtexec), deploy with command line, Azure Data Studio, or Visual Studio Code, and consume them with an interactive, RESTful API schedule.":::

The followings are the target scenarios for app deploy:

- Deploy Python or R web services inside the big data cluster to address variety of use cases such as machine learning inferencing, API serving, etc.
- Create a machine learning inferencing endpoint using the MLeap engine.
- Schedule and run packages from DTSX files using dtexec utility for data transformation and movement.

### Use app deploy Python runtime

In app deploy, BDC python runtime allows Python application inside the big data cluster to address variety of use cases such as machine learning inferencing, API serving and more.

The app deploy Python runtime uses Python 3.8 on SQL Server Big Data Clusters CU10+.

In app deploy, `spec.yaml` is where you provide the information that controller needs to know to deploy your application. The following are the fields that can be specified:

- `name`: the application name
- `version`: the application version, for instance, such as `v1`
- `runtime`: the app deploy runtime, you need to specify it as: `Python`
- `src`: the path to the Python application
- `entry point`: the entry point function in the src script to execute for this Python application.

Aside from above you need to specify the input and output of your Python application. That generates a `spec.yaml` file similar to the following:

```yaml
#spec.yaml
name: add-app
version: v1
runtime: Python
src: ./add.py
entrypoint: add
replicas: 1
poolsize: 1
inputs:
  x: int
  y: int
output:
  result: int
```

You can create the basic folder and file structure needed to deploy a Python app running on Big Data Clusters:

```console
azdata app init --template python --name hello-py --version v1
```

For next steps, see [How to deploy an app on SQL Server Big Data Clusters](app-create.md).

#### Limitations of app deploy Python runtime

App deploy Python runtime doesn't support scheduling scenario. Once Python app is deployed, and running in BDC, a RESTful endpoint is configured to listen for incoming requests.

### Use app deploy R runtime

In app deploy, BDC Python runtime allows R application inside the big data cluster to address variety of use cases such as machine learning inferencing, API serving and more.

The app deploy R runtime uses Microsoft R Open (MRO) version 3.5.2 on SQL Server Big Data Clusters CU10+.

#### How to use it?

In app deploy, `spec.yaml` is where you provide the information that controller needs to know to deploy your application. The following are the fields that can be specified:

- `name`: the application name
- `version`: the application version, for instance, such as `v1`
- `runtime`: the app deploy runtime, you need to specify it as: `R`
- `src`: the path to the R application
- `entry point`: the entry point to execute this R application

Aside from above you need to specify the input and output of your R application. That generates a `spec.yaml` file similar to the following:

```yaml
#spec.yaml
name: roll-dice
version: v1
runtime: R
src: ./roll-dice.R
entrypoint: rollEm
replicas: 1
poolsize: 1
inputs:
  x: integer
output:
  result: data.fram
```

You can create the basic folder and file structure needed to deploy a new R application using the following command:

```console
azdata app init --template r --name hello-r --version v1
```

For next steps, see [How to deploy an app on SQL Server Big Data Clusters](app-create.md).

#### Limitations of R runtime

These limitations align with the [Microsoft R Application Network](https://mran.microsoft.com/open).

### Using app deploy dtexec runtime

In app deploy, the Big Data Cluster runtime integrated dtexec utility is from SSIS on Linux (mssql-server-is). App deploy uses the dtexec utility to load packages from *.dtsx files. It supports running SSIS packages on cron-style schedule or on-demand through web service requests.

This feature uses `/opt/ssis/bin/dtexec /FILE` from SQL Server 2019 Integration Service on Linux. It supports dtsx format for [SQL Server 2019 Integration Service on Linux (mssql-server-is 15.0.2)](../linux/sql-server-linux-setup-ssis.md). To learn more about dtexec utility, see [dtexec Utility](../integration-services/packages/dtexec-utility.md).

In app deploy, `spec.yaml` is where you provide the information that controller needs to know to deploy your application. The following are the fields that can be specified:

- `name`: the application `name`
- `version`: the application version, for instance, such as `v1`
- `runtime`: the app deploy runtime, in order to run dtexec utility, you need to specify it as: `SSIS`
- `entrypoint`: specify an entry point, this is usually your .dtsx file in our case.
- `options`: specify additional options for `/opt/ssis/bin/dtexec /FILE`, for example to connect to a database with connection string, it would follow the following pattern: 

   ```console
   /REP V /CONN "sqldatabasename"\;"\"Data Source=xx;User ID=xx;Password=xx\""
   ```

  For details on syntax, see [dtexec Utility](../integration-services/packages/dtexec-utility.md).

- `schedule`: specify how often the job needs to be executed, for instances, when using cron expression to specify this value specifies as  "*/1 * * * *" meaning  the job is being executed on minute basis.

You can create the basic folder and file structure needed to deploy a new SSIS application using the following command:

```console
azdata app init --name hello-is –version v1 --template ssis                                 
```

That generates a `spec.yaml` file to the following:

```yaml
#spec.yaml
entrypoint: ./hello.dtsx
name: hello-is
options: /REP V
poolsize: 2
replicas: 2
runtime: SSIS
schedule: '*/2 * * * *'
version: v1
```

The example also creates a sample `hello.dtsx` package.

All of your app files are in the same directory as your `spec.yaml`. The `spec.yaml` must be at the root level of your app source code directory including the dtsx file.

For next steps, see [How to deploy an app on SQL Server Big Data Clusters](app-create.md).

#### Limitations of the dtexec utility runtime

All limitations and known issues for SQL Server Integration Services (SSIS) on Linux are applicable in [!INCLUDE[ssbigdataclusters-ss-nover](../includes/ssbigdataclusters-ss-nover.md)]. You can find out more from [Limitations and known issues for SSIS on Linux](../linux/sql-server-linux-ssis-known-issues.md).

### Using app deploy MLeap runtime

The app deploy MLeap runtime supports MLeap Serving v0.13.0.

In app deploy, `spec.yaml` is where you provide the information that controller needs to know to deploy your application. The following are the fields that can be specified:

- `name`: the application name 
- `version`: the application version, for instance, such as `v1` 
- `runtime`: the app deploy runtime, you need to specify it as: `Mleap`

Aside from above you need to specify the `bundleFileName` of your MLeap application. That generates a `spec.yaml` file similar to the following:

```yaml
#spec.yaml
name: mleap-census
version: v1
runtime: Mleap
bundleFileName: census-bundle.zip
replicas: 1
```

You can create the basic folder and file structure needed to deploy a new MLeap application using the following command:

```console
azdata app init --template mleap --name hello-mleap --version v1
```

For next steps, see [How to deploy an app on SQL Server Big Data Clusters](app-create.md).

#### Limitations of MLeap runtime

The limitations align with the vision from the MLeap open-source project [Combust on GitHub](https://github.com/combust/mleap).

## Next steps

To learn more about how to create and run applications on [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following:

- [Deploy applications using azdata](app-create.md)
- [Deploy applications using the app deploy extension](app-deployment-extension.md)
- [Consume applications on Big Data Clusters](app-consume.md)

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following overview:

- [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md)
