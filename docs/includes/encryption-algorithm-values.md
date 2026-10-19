---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/12/2026
ms.service: sql
ms.topic: include
---

## Encryption algorithm values

The following table describes the possible values for the encryption algorithm.

| `encryption_algorithm` | `encryption_algorithm_desc` | Corresponding DDL option |
| --- | --- | --- |
| `0` | None | Disabled |
| `1` | `RC4` | Required algorithm RC4 |
| `2` | `AES` | Required algorithm AES |
| `3` | None, `RC4` | Supported algorithm RC4 |
| `4` | None, `AES` | Supported algorithm AES |
| `5` | `RC4`, `AES` | Required algorithm RC4 AES |
| `6` | `AES`, `RC4` | Required algorithm AES RC4 |
| `7` | None, `RC4`, `AES` | Supported algorithm RC4 AES |
| `8` | None, `AES`, `RC4` | Supported algorithm AES RC4 |

The RC4 algorithm is only supported for backward compatibility. New material can only be encrypted using `RC4` or `RC4_128` when the database is in compatibility level `90` or `100` (not recommended). Use one of the AES algorithms instead. In [!INCLUDE [sssql11-md](sssql11-md.md)] and later versions, material encrypted using `RC4` or `RC4_128` can be decrypted in any compatibility level.
