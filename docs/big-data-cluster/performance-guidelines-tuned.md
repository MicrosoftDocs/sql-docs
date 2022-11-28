---
title: Performance best practices for SQL Server Big Data Clusters
description: This article provides performance best practices and guidelines for running SQL Server Big Data Clusters on Kubernetes
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/30/2020
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Performance best practices and configuration guidelines for SQL Server Big Data Clusters

[!INCLUDE [sqlserver2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article provides best practices and recommendations to maximize performance for applications that target services running within a big data cluster.

The following guidelines focus on recommendations for configuring the Linux operating system hosting the Kubernetes worker nodes where the big data cluster will be deployed. As a best practice, configure the tuning profile before deploying the big data cluster. The settings included in the proposed tuning profile were validated during the case study conducted by Microsoft and Intel. The results of the study are published for download in this [whitepaper](https://aka.ms/sql-bdc-spark-perf/).

> [!TIP]
> For tuning configurations specific to SQL Server on Linux, see [Performance best practices and configuration guidelines for SQL Server on Linux](../linux/sql-server-linux-performance-best-practices.md). Also, other best practices like index design for SQL Server databases, still apply.

## Proposed Linux settings using a tuned `mssql-bdc` profile

Create a **tuned.conf** configuration profile with the content below.

```bash
[main]
summary=Optimize for Microsoft SQL Server Big Data Clusters TPC-DS performance
include=throughput-performance
 
[sysctl]
#network tunings
net.ipv4.conf.default.rp_filter=1
net.ipv4.tcp_timestamps=0
net.ipv4.tcp_sack = 1
net.core.netdev_max_backlog = 25000
net.core.rmem_max = 2147483647
net.core.wmem_max = 2147483647
net.core.rmem_default = 33554431
net.core.wmem_default = 33554432
net.core.optmem_max = 33554432
net.ipv4.tcp_rmem =8192 33554432 2147483647
net.ipv4.tcp_wmem =8192 33554432 2147483647
net.ipv4.tcp_low_latency=1
net.ipv4.tcp_adv_win_scale=1
net.ipv6.conf.all.disable_ipv6 = 1
net.ipv6.conf.default.disable_ipv6 = 1
net.ipv4.conf.all.arp_filter=1
net.ipv4.tcp_retries2=5
net.ipv6.conf.lo.disable_ipv6 = 1
net.core.somaxconn = 65535
 
#memory cache settings
vm.swappiness=1
vm.overcommit_memory=0
vm.dirty_background_ratio=1
 
#kernel NUMA
kernel.numa_balancing=0

#filesystem
fs.aio-max-nr=1048576
 
[vm]
#should be revisited for SQL large pages use in master/data/compute pods
transparent_hugepages=never
```

## Install **tuned** utility on all the Kubernetes worker nodes

To install **tuned**, execute:

```bash
apt-get -y install tuned
```

## Apply tuning settings to all Kubernetes worker nodes

On each of the targeted worker nodes, copy the **tuned.conf** file created above:

```bash
cd /usr/lib/tuned
scp -r <sourcePath> ./mssql-bdc
```

To enable this **mssql-bdc** tuned profile, save these definitions in a **tuned.conf** file under a `/usr/lib/tuned/mssql-bdc` folder on all the Kubernetes worker nodes and enable the profile using:

```bash
chmod +x /usr/lib/tuned/mssql-bdc/tuned.conf
tuned-adm profile mssql-bdc
```

Verify it's enabled using this command:

```bash
tuned-adm active
```

or

```bash
tuned-adm list
```

If the profile is changed dynamically, for the new changes to take effect, you must restart **tuned** on all the impacted worker nodes:

```bash
systemctl restart tuned
```
 
Logs for **tuned** service can be found at */var/log/tuned/tuned.log*.

Optionally, you can configure the tuning profile on one node in the Kubernetes cluster and use the script below to copy it over and configure on the remaining nodes.

```bash
#!/bin/bash -e
# This script takes a list of servers (IPs like `cat ~administrator/workerhosts)) as input
# and update these servers with the local version of mssql-bdc tuned.conf.
 
is_root() {
    local is_root_set=0
    if [ "$EUID" -ne 0 ]; then
        echo "Please run as root"
    else
        is_root_set=1
    fi
    return "${is_root_set}"
}
 
# function main
if is_root -eq 0; then
    exit 0
fi
 
while [ $# -gt 0 ]
do
    # sometimes, people add non-breaking space characters to their *host* files.
    WORKER_IP=$(echo "$1" | sed -e 's/\xC2\xA0//g')
    echo -e "updating mssql-bdc tuned on \e[42m${WORKER_IP}\e[0m"
    # the following commands assume tuned was not set up and active...
    # TODO: add the tuned install and status checks
    ssh "${WORKER_IP}" mkdir -p /usr/lib/tuned/mssql-bdc
    scp ~administrator/tuned.conf "${WORKER_IP}":/usr/lib/tuned/mssql-bdc/tuned.conf
    ssh "${WORKER_IP}" tuned-adm profile mssql-bdc
    ssh "${WORKER_IP}" systemctl restart tuned
    ssh "${WORKER_IP}" tuned-adm active
    shift
done

```

## Next steps

For more resources including reference architectures for SQL Server Big Data Clusters, see:

* [Case Study: SQL Workloads running on Apache Spark in MS SQL Server 2019 Big Data Cluster](https://aka.ms/sql-bdc-spark-perf/)

* [HPE Reference Architecture for delivering insight across all your data with Microsoft SQL Server 2019 Big Data Clusters](https://h20195.www2.hpe.com/V2/GetDocument.aspx?docname=a50001963enw)

* [Dell EMC PowerStore: Microsoft SQL Server 2019 Big Data Clusters](https://www.dellemc.com/resources/en-us/asset/white-papers/products/storage/h18231-dell-emc-powerstore-sql-server-big-data-clusters.pdf)

* [Microsoft SQL Server 2019 Big Data Clusters: A Big Data Solution Using Dell EMC Infrastructure](https://infohub.delltechnologies.com/t/microsoft-sql-server-2019-big-data-clusters-a-big-data-solution-using-dell-emc-infrastructure/)

* [Microsoft SQL Server 2019 Big Data Clusters on Cisco UCS Reference Architecture](https://www.cisco.com/c/en/us/solutions/collateral/data-center-virtualization/unified-computing/sql-server-on-big-data-cluster-on-ucs.html)