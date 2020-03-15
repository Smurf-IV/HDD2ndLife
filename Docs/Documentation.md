# HDD 2nd Life

Give your HDD one last job with "Removed Bad Sector Degredation Areas" via Marked Unusable Partitioning.



## What is this ?
As hard Drives get used their "reallocatable sectors" become used up, therefore the disk "wears out". 
The idea behind this application, is to find these "wear spots" and remove them from useable space via the use of partitions
at a high level.


## Why?
This is just to allow data that does not change often to be stored on a hard drive, for backup purposes. 
Do not assume that the drive will remain "happy" during further R/W cycles, just that it can be used as a snapshot for Software raid, or even just as a removeable copy disk.

## But HDD's are cheap!
Yes, and No. If you have invested money in a large disk, and you just want to throw it away, then fine (Recycle it!).
This application, just allows you to get more "Return On Your Invest", before the physical recycle process is needed.


## How does it work?
- It lists the disks with information about them
- It then allows you to select a disk
- Once selected it will allow a Surface test to be performed to see if there are "Bad Sectors"
- Then it will allow you to perform a partitioning scheme to "hide" the wear areas away from the useable space on your disk


## Aren't "Bad Sectors" created by power failure writes ?
Yes they can be, and as such they can be recovered by performing a write over, but if the "SMART" data flags of the hard drive are signalling any "Uncorrectable Sector Counts" or high "Reallocated Sector Counts", then this is an indication of the disk wearing out at those locations.
There are two types of bad sectors — often divided into “physical” and “logical” bad sectors or “hard” and “soft” bad sectors.


## The Partitioning Scheme - Is it data destructive ?
_Yes_ it will be !

## There's another tool that does this, Why have you written yours ?
I used to use that other tool, but when the HDD's got above 1TB, then the calculations for progress and completion becoma nonsense (i.e. -1000998765% to complete).
It also did not have a very interactive display, which did not show the areas that would be excluded, when entering partition scheme values (they had to be entered before starting).

## I have heard that a Low-Level Format makes things work
I've heard that as well, and I have tried it on drives that start to report SMART count errors, But it always seems that the warnings go away, and then the drive fails anyway, just when you don't want it to, which means that your data has been in jepordy twice (1st time to identify that a LLFormat is required, then again with the sudden failure!)

# Further reading:
- https://en.wikipedia.org/wiki/Hard_disk_drive_failure#Metrics_of_failures
- https://www.howtogeek.com/173463/bad-sectors-explained-why-hard-drives-get-bad-sectors-and-what-you-can-do-about-it/
- https://www.uwe-sieber.de/drivetools_e.html#drivecleanup
- https://gsmartcontrol.sourceforge.io/home/
- https://www.hdsentinel.com/help/en/61_surfacetest.html
- [HDDScan – FREE HDD Test Diagnostics Software with RAID and USB Flash support](https://hddscan.com/)