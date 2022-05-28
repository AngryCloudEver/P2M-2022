# FAQ

# Requirements
.NET Core 3.1

1. Browse https://dotnet.microsoft.com/en-us/download/dotnet/3.1/runtime?cid=getdotnetcore
2. Click "Download x64" that is located beneath the category "Run Console Apps" and download the exe program
3. Run the exe program for installation 

## Downloading the Program
1. Browse https://github.com/AngryCloudEver/P2M-2022
2. Click the green button named "Code"
3. Click button "Download ZIP" and download the zip folder
4. Extract the zip folder on desired location


## How to run?
[Recommended] Method 1: via Command Prompt
1. Open CMD
2. `cd "C:\wherever\project\is\bin\Debug\netcoreapp3.1"`
3. `".\Prototype 1.exe"`

Method 2: via exe
1. Inside the zip folder, navigate through "P2M-2022-main\PrototypeGame 1\bin\Debug\netcoreapp3.1"
2. Double-click the file "PrototypeGame 1.exe", which is located inside folder "netcoreapps3.1"

## Editing Data Values (for Game Designers)<br/>
Navigate to C:\Unity Projects\P2M-2022\PrototypeGame 1\bin\Debug\netcoreapp3.1 <br/>

`power.txt` <br/>
Format: (string powerName, int powerCost, int powerPollution, int powerPlayerAmount) <br/>
`food.txt` <br/>
Format: (int foodPowerCost, int foodMoneyCost, int foodPlayerAmount, int foodProduce) <br/>
`stats.txt` <br/>
Format: (string title, int value)<br/>
`policy.txt` <br/>
Format: (string title, int cashCost, int popularity, int cashEffectAccept, int foodEffectAccept, int powerEffectAccept, int pollutionEffectAccept, int industryEffectAccept, int reputationEffectAccept, int cashEffectReject, int foodEffectReject, int powerEffectReject, int pollutionEffectReject, int industryEffectReject, int reputationEffectReject)
`policyDescription.txt` <br/>
Format: (string title, string description) <br/>

**Note: DO NOT change any name/title and the amount of data per line as well as comma seperators** <br/> <br/>
Each stat/object's value is defined in a single line <br/>
