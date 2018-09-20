---
title: Create deployment scripts for SQL Server Always On Availability Group on Kubernetes
description: This article explains how to create deployment scripts for a SQL Server Always On Availability Group on Kubernetes 
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 09/24/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Create deployment script for SQL Server Always On Availability Group 

This article describes how to create the `.yaml` files that you can use as manifests to deploy a SQL Server Availability Group on a Kubernetes cluster. 

## Before you start

Install the following tools on your workstation.

* [Python](https://www.python.org/downloads/) (3.5 or 3.6)
* [PyYAML](https://pyyaml.org/) - Python Packages
* [Kubernetes Client](https://github.com/kubernetes-client/python) - Python Package

Add python paths to the enviornment variables (for Windows).

## Install the Required Components

The following example The above installs the PyYAML and Kubernetes Client packages for Python.

```cmd
pip install --user -r "C:\<path>\requirements.txt"
```

## Create Cluster and download config file

The following example creates the cluster in Azure Kubernetes Service (AKS).

Before you run the script, update the values in angle brackets - `<>`. 

```azcli
az aks create  --resource-group <GroupName> --name <ClusterName> --generate-ssh-keys --node-count 4 --node-vm-size "Standard_D4s_v3" --kubernetes-version 1.11.1

az aks get-credentials --resource-group=<GroupName> --name=<ClusterName>
```

## Run the Script

The following examples demonstrate how to run the scripts.

### Example - help

```python
   ./deploy-ag.py --help
```

**usage**: deploy-ag.py [-h] {deploy,failover} ...

  **optional arguments**:

    -h, --help show this help message and exit

  **subcommands**:
    Actions on k8s agent

      {deploy,failover}
        deploy
          Deploy a set of SQL Servers in an Availability Group
        failover
        Perform a failover to a target replica.

Example  
```python
./deploy-ag.py deploy --help

usage: deploy-ag.py deploy [-h] [--verbose] [--ag AG] [-n NAMESPACE]
                           [--dry-run] [-s SQL_SERVERS [SQL_SERVERS ...]]
                           [-p SA_PASSWORD] [-e {ON_PREM,AKS}]
                           [--skip-create-namespace]

Deploy SQL Server and k8s Agents in namespace(AG name)

		optional arguments:
		  -h, --help            show this help message and exit
		  --verbose, -v         Verbosity of output
		  --ag AG               name of the Availability Group. Default=ag1
		  -n NAMESPACE, --namespace NAMESPACE
								name of the k8s namespace. Defaults to AG name if not
								specified.
		  --dry-run             Perform a dry run and not apply the specs.
		  -s SQL_SERVERS [SQL_SERVERS ...], --sql-servers SQL_SERVERS [SQL_SERVERS ...]
								names of SQL Server instances(up to 5, separated by
								spaces)Default=['mssql1', 'mssql2', 'mssql3']
		  -p SA_PASSWORD, --sa-password SA_PASSWORD
								SA Password. Default='SAPassword2018'
		  -e {ON_PREM,AKS}, --env {ON_PREM,AKS}
		  --skip-create-namespace
								Skip namespace creation.
```
								
Example 3 --  

```python
./deploy-ag.py failover --help
			usage: deploy-ag.py failover [-h] [--verbose] [--ag AG]
										 [--namespace NAMESPACE] [--dry-run]
										 target_replica

			Perform a manual failover

			positional arguments:
			  target_replica        name of target SQL Server replica to failover to

			optional arguments:
			  -h, --help            show this help message and exit
			  --verbose, -v         Verbosity of output
			  --ag AG               name of the Availability Group. Default=ag1
			  --namespace NAMESPACE
									name of the k8s namespace. Defaults to AG name if not
									specified
			  --dry-run             Perform a dry run and NOT apply the specs							
```

### Create the specs but **NOT** apply

```python
	./deploy-ag.py deploy --dry-run
	
	python.exe ./deploy-ag.py deploy --ag ag1 --namespace AG1 --sql-servers ['SQL1', 'SQL2', 'SQL3'] --sa-password 'SAPassword2018' --env AKS --dry-run

	[ALL] Created the following specs:
	[ALL]    C:<path>\kube_agent_deploy-uxgte0o9ag1\operator.yaml
	[ALL]    C:<path>\kube_agent_deploy-uxgte0o9ag1\sql-secrets.yaml
	[ALL]    C:<path>\kube_agent_deploy-uxgte0o9ag1\pv.yaml
	[ALL]    C:<path>\kube_agent_deploy-uxgte0o9ag1\sqlserver.yaml
	[ALL]    C:<path>\kube_agent_deploy-uxgte0o9ag1\ag-services.yaml
	[ALL]
	[ALL] Wrote spec paths: 'deploy_ag1_specs'
	[32m./deploy-ag.py exitcode: 0[0m
```

<> This will generate the sample yaml files in the Temp directory. 
	
### Create the Specs and apply

```python
	python.exe ./deploy-ag.py deploy --ag ag1 --namespace ag1 --sa-password '!Locks123' --env AKS --verbose
```	

### Perform a Manual Failover

```python
	./deploy-ag.py failover --ag ag1 --namespace ag1 --verbose mssql1-0
```

  ## Next steps

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)