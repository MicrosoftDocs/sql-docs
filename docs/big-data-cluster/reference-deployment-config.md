---
title: Deployment configuration file reference
titleSuffix: SQL Server big data clusters
description: Reference for a big data cluster deployment configuration file.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/23/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# Deployment configuration file reference for big data clusters

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article provides a JSON file that documents the structure of a SQL Server 2019 big data cluster deployment configuration file.

> [!TIP]
> Do not use this as your actual deployment configuration file. Instead, follow the instructions in the [deployment guidance](deployment-guidance.md#configfile) for how to work with configuration files.

## Deployment configuration file

Use the following JSON file as a reference for the structure and settings in a big data cluster deployment configuration file.

```json
{
  "metadata": {
    "kind": {
      "name": "Deployment type",
      "description": "The type of deployment - in this case a 'Cluster'. Do not change this value."
    },
    "name": {
      "name": "Cluster Name",
      "description": "SQL Server big data cluster name. This is also the name of the Kubernetes namespace to deploy SQLServer big data cluster into."
    }
  },
  "spec": {
    "controlPlane": {
      "spec": {
        "docker": {
          "registry": {
            "name": "Docker Registry",
            "description": "The private Docker registry where the images used to deploy the SQL Server big data cluster are stored."
          },
          "repository": {
            "name": "Docker Repository",
            "description": "The private Docker repository within the above registry where images used to deploy the big data cluster are stored."
          },
          "imageTag": {
            "name": "Docker Image Tag",
            "description": "The Docker image tag used for the Docker images used for deploying the SQL Server big data cluster."
          },
          "imagePullPolicy": {
            "name": "Docker Image Pull Policy",
            "description": "The Docker image pull policy."
          }
        },
        "storage": {
          "usePersistentVolume": {
            "name": "Kubernetes Persistent Volume Use",
            "description": "Set this value to `true` to use Kubernetes Persistent Volume Claims for storage. Also specify the className in this case. Set this value to `false` to use ephemeral host storage for non production deployments."
          },
          "className": {
            "name": "Kubernetes Storage Class",
            "description": "If usePersistentVolume is `true` this indicates the name of the Kubernetes Storage Class to use. You must pre-provision the storage class and the persistent volumes or you can use a built in storage class if the platform you are deploying provides this capability."
          },
          "accessMode": {
            "name": "Kubernetes Persistent Volume Access Mode",
            "description": "Access mode for the Persistent Volume. Default value is ReadWriteOnce."
          },
          "size": {
            "name": "Kubernetes Persistent Volume Claim Size",
            "description": "The size of each Persisted Volume Claim created. Default value is 10Gi."
          }
        },
        "endpoints": [
          {
            "name": {
              "name": "Controller Endpoint Name",
              "description": "Name of Kubernetes service created for controller endpoint."
            },
            "serviceType": {
              "name": "Controller Service Type",
              "description": "Kubernetes service type for controller service. Possible values are LoadBalancer or NodePort."
            },
            "port": {
              "name": "Controller Service Port",
              "description": "The TCP/IP port that the controller service listens on the public network."
            }
          },
          {
            "name": {
              "name": "Management Proxy Endpoint Name",
              "description": "Name of Kubernetes service created for management proxy endpoint."
            },
            "serviceType": {
              "name": "Management Proxy Service Type",
              "description": "Kubernetes service type for management proxy service. Possible values are LoadBalancer or NodePort."
            },
            "port": {
              "name": "Management Proxy Service Port",
              "description": "The TCP/IP port that proxy service listens on the public network. This is the port used for computing the portal URL."
            }
          },
          {
            "name": {
              "name": "App Proxy Endpoint Name",
              "description": "Name of Kubernetes service created for app proxy endpoint."
            },
            "serviceType": {
              "name": "App Proxy Service Type",
              "description": "Kubernetes service type for proxy service. Possible values are LoadBalancer or NodePort."
            },
            "port": {
              "name": "App Proxy Service Port",
              "description": "The TCP/IP port that proxy service listens on the public network."
            }
          },
          {
            "name": {
              "name": "Gateway Endpoint Name",
              "description": "Name of Kubernetes service created for gateway endpoint."
            },
            "serviceType": {
              "name": "Gateway Service Type",
              "description": "Kubernetes service type for gateway service. Possible values are LoadBalancer or NodePort."
            },
            "port": {
              "name": "Gateway Service Port",
              "description": "The TCP/IP port that gateway service listens on the public network."
            }
          }
        ]
      },
    "pools": [
      {
        "metadata": {
          "kind": {
            "name": "Deployment Type",
            "description": "The type of deployment - in this case a Pool"
          },
          "name": {
            "name": "Pool Name",
            "description": "The name of the pool. `Default` is only allowed value in current version."
          }
        },
        "spec": {
          "type": {
            "name": "Pool Type",
            "description": "Master"
          },
          "replicas": {
            "name": "Master Pool Replicas",
            "description": "The number of replicas you would like in Master pool"
          },
          "storage": {
            "usePersistentVolume": {
              "name": "Kubernetes Persistent Volume Use",
              "description": "Set this value to `true` to use Kubernetes Persistent Volume Claims for storage. Also specify the className in this case. Set this value to `false` to use ephemeral host storage for non production deployments."
            },
            "className": {
              "name": "Kubernetes Storage Class",
              "description": "If usePersistentVolume is `true` this indicates the name of the Kubernetes Storage Class to use. You must pre-provision the storage class and the persistent volumes or you can use a built in storage class if the platform you are deploying provides this capability."
            },
            "accessMode": {
              "name": "Kubernetes Persistent Volume Access Mode",
              "description": "Access mode for the Persistent Volume. Default value is ReadWriteOnce."
            },
            "size": {
              "name": "Kubernetes Persistent Volume Claim Size",
              "description": "The size of each Persisted Volume Claim created. Default value is 10Gi."
            }
          },
          "endpoints": [
            {
              "name": {
                "name": "Master Endpoint Name",
                "description": "Endpoint name of Master pool"
              },
              "serviceType": {
                "name": "Master Pool Endpoint Type",
                "description": "Endpoint type of Master Pool - NodePort"
              },
              "port": {
                "name": "Master Pool Port",
                "description": "The port for the master endpoint"
              }
            }
          ]
        },
        "hadoop": {
          "yarn": {
            "nodeManager": {
              "memory": {
                "name": "Yarn Node Manager Memory",
                "description": "The standard memory is 18432"
              },
              "vcores": {
                "name": "Yarn Node Manager Virtual Cores",
                "description": "The standard virtual cores is 6"
              }
            },
            "schedulerMax": {
              "memory": {
                "name": "Yarn Scheduler Max Memory",
                "description": "The standard memory is 18432"
              },
              "vcores": {
                "name": "Yarn Scheduler Max Virtual Cores",
                "description": "The standard virtual cores is 6"
              }
            },
            "capacityScheduler": {
              "maxAmPercent": {
                "name": "Capacity Scheduler Max Percent",
                "description": "The standard max percentage is 0.3"
              }
            }
          },
          "spark": {
            "driverMemory": {
              "name": "Spark Driver Memory",
              "description": "The standard Spark driver memory is 2g"
            },
            "driverCores": {
              "name": "Spark Driver Cores",
              "description": "The standard Spark driverCores is 1"
            },
            "executorInstances": {
              "name": "Spark Executor Instances",
              "description": "The standard Spark executor instances is 3"
            },
            "executorMemory": {
              "name": "Spark Executor Memory",
              "description": "The standard Spark executor memory is 1536m"
            },
            "executorCores": {
              "name": "Spark Executor Cores",
              "description": "The standard Spark executor cores is 1"
            }
          }
        }
      },
      {
        "metadata": {
          "kind": {
            "name": "Deployment Type",
            "description": "The type of deployment - in this case a Pool"
          },
          "name": {
            "name": "Pool Name",
            "description": "The name of the pool. `Default` is only allowed value in current version."
          }
        },
        "spec": {
          "type": {
            "name": "Pool Type",
            "description": "Compute"
          },
          "replicas": {
            "name": "Compute Pool Replicas",
            "description": "The number of replicas you would like in Compute pool"
          },
          "storage": {
            "usePersistentVolume": {
              "name": "Kubernetes Persistent Volume Use",
              "description": "Set this value to `true` to use Kubernetes Persistent Volume Claims for storage. Also specify the className in this case. Set this value to `false` to use ephemeral host storage for non production deployments."
            },
            "className": {
              "name": "Kubernetes Storage Class",
              "description": "If usePersistentVolume is `true` this indicates the name of the Kubernetes Storage Class to use. You must pre-provision the storage class and the persistent volumes or you can use a built in storage class if the platform you are deploying provides this capability."
            },
            "accessMode": {
              "name": "Kubernetes Persistent Volume Access Mode",
              "description": "Access mode for the Persistent Volume. Default value is ReadWriteOnce."
            },
            "size": {
              "name": "Kubernetes Persistent Volume Claim Size",
              "description": "The size of each Persisted Volume Claim created. Default value is 10Gi."
            }
          }
        }
      },
      {
        "metadata": {
          "kind": {
            "name": "Deployment Type",
            "description": "The type of deployment - in this case a Pool"
          },
          "name": {
            "name": "Pool Name",
            "description": "The name of the pool. `Default` is only allowed value in current version."
          }
        },
        "spec": {
          "type": {
            "name": "Pool Type",
            "description": "Data"
          },
          "replicas": {
            "name": "Data Pool Replicas",
            "description": "The number of replicas you would like in Data pool"
          },
          "storage": {
            "usePersistentVolume": {
              "name": "Kubernetes Persistent Volume Use",
              "description": "Set this value to `true` to use Kubernetes Persistent Volume Claims for storage. Also specify the className in this case. Set this value to `false` to use ephemeral host storage for non production deployments."
            },
            "className": {
              "name": "Kubernetes Storage Class",
              "description": "If usePersistentVolume is `true` this indicates the name of the Kubernetes Storage Class to use. You must pre-provision the storage class and the persistent volumes or you can use a built in storage class if the platform you are deploying provides this capability."
            },
            "accessMode": {
              "name": "Kubernetes Persistent Volume Access Mode",
              "description": "Access mode for the Persistent Volume. Default value is ReadWriteOnce."
            },
            "size": {
              "name": "Kubernetes Persistent Volume Claim Size",
              "description": "The size of each Persisted Volume Claim created. Default value is 10Gi."
            }
          }
        }
      },
      {
        "metadata": {
          "kind": {
            "name": "Deployment Type",
            "description": "The type of deployment - in this case a Pool"
          },
          "name": {
            "name": "Pool Name",
            "description": "The name of the pool. `Default` is only allowed value in current version."
          }
        },
        "spec": {
          "type": {
            "name": "Pool Type",
            "description": "Storage"
          },
          "replicas": {
            "name": "Storage Pool Replicas",
            "description": "The number of replicas you would like in Storage pool"
          },
          "storage": {
            "usePersistentVolume": {
              "name": "Kubernetes Persistent Volume Use",
              "description": "Set this value to `true` to use Kubernetes Persistent Volume Claims for storage. Also specify the className in this case. Set this value to `false` to use ephemeral host storage for non production deployments."
            },
            "className": {
              "name": "Kubernetes Storage Class",
              "description": "If usePersistentVolume is `true` this indicates the name of the Kubernetes Storage Class to use. You must pre-provision the storage class and the persistent volumes or you can use a built in storage class if the platform you are deploying provides this capability."
            },
            "accessMode": {
              "name": "Kubernetes Persistent Volume Access Mode",
              "description": "Access mode for the Persistent Volume. Default value is ReadWriteOnce."
            },
            "size": {
              "name": "Kubernetes Persistent Volume Claim Size",
              "description": "The size of each Persisted Volume Claim created. Default value is 10Gi."
            }
          }
        },
        "namenode": {
          "replicas": {
            "name": "Hadoop Replicas",
            "description": "The number of replicas for hadoop"
          },
          "spec": {
            "storage": {
              "usePersistentVolume": {
                "name": "Kubernetes Persistent Volume Use",
                "description": "Set this value to `true` to use Kubernetes Persistent Volume Claims for storage. Also specify the className in this case. Set this value to `false` to use ephemeral host storage for non production deployments."
              },
              "className": {
                "name": "Kubernetes Storage Class",
                "description": "If usePersistentVolume is `true` this indicates the name of the Kubernetes Storage Class to use. You must pre-provision the storage class and the persistent volumes or you can use a built in storage class if the platform you are deploying provides this capability."
              },
              "accessMode": {
                "name": "Kubernetes Persistent Volume Access Mode",
                "description": "Access mode for the Persistent Volume. Default value is ReadWriteOnce."
              },
              "size": {
                "name": "Kubernetes Persistent Volume Claim Size",
                "description": "The size of each Persisted Volume Claim created. Default value is 10Gi."
              }
            }
          }
        },
        "hadoop": {
          "yarn": {
            "nodeManager": {
              "memory": {
                "name": "Yarn Node Manager Memory",
                "description": "The standard memory is 18432"
              },
              "vcores": {
                "name": "Yarn Node Manager Virtual Cores",
                "description": "The standard virtual cores is 6"
              }
            },
            "schedulerMax": {
              "memory": {
                "name": "Yarn Scheduler Max Memory",
                "description": "The standard memory is 18432"
              },
              "vcores": {
                "name": "Yarn Scheduler Max Virtual Cores",
                "description": "The standard virtual cores is 6"
              }
            },
            "capacityScheduler": {
              "maxAmPercent": {
                "name": "Capacity Scheduler Max Percent",
                "description": "The standard max percentage is 0.3"
              }
            }
          },
          "spark": {
            "driverMemory": {
              "name": "Spark Driver Memory",
              "description": "The standard Spark driver memory is 2g"
            },
            "driverCores": {
              "name": "Spark Driver Cores",
              "description": "The standard Spark driverCores is 1"
            },
            "executorInstances": {
              "name": "Spark Executor Instances",
              "description": "The standard Spark executor instances is 3"
            },
            "executorMemory": {
              "name": "Spark Executor Memory",
              "description": "The standard Spark executor memory is 1536m"
            },
            "executorCores": {
              "name": "Spark Executor Cores",
              "description": "The standard Spark executor cores is 1"
            }
          }
        }
      }
    ]
  }
}
```

## Next steps

For more information on how to use and customize deployment configuration files, see [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md#configfile).
