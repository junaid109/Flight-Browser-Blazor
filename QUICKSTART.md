# 🚀 Quick Start Guide

## Prerequisites Check
```powershell
# Verify .NET SDK
dotnet --version
# Should show 10.0.x or later
```

## 🎯 3-Step Launch

### Step 1: Build the Solution
```powershell
dotnet build
```

### Step 2: Start All Services
```powershell
.\start.ps1
```

This will open 3 terminal windows:
- **Terminal 1**: Flight API on `http://localhost:5000`
- **Terminal 2**: Flight Simulator (connects to API)
- **Terminal 3**: Dashboard on `http://localhost:5001`

### Step 3: Open the Dashboard
Wait 5-10 seconds for all services to start, then open:

**👉 http://localhost:5001**

## 🎨 What You'll See

### Dashboard Layout
```
┌─────────────────┬──────────────────────┬─────────────────┐
│  Active Flights │    Radar Display     │ System Stats    │
│                 │                      │                 │
│  ✈ AA101       │    [Radar Map]       │ 📊 5 Flights   │
│  ✈ BA202       │    with planes       │ ✅ NORMAL      │
│  ✈ DL303       │    moving in         │ 🌤 Weather     │
│  ✈ UA404       │    real-time         │                 │
│  ✈ AF505       │                      │                 │
└─────────────────┴──────────────────────┴─────────────────┘
```

## 🎮 Interactive Features

1. **Click any flight** in the left sidebar to track it
2. **Watch planes move** on the radar map in real-time
3. **See live updates** of altitude, speed, and position
4. **Hover over flights** for smooth animations

## 🔍 Testing the API

### Get All Flights
```powershell
# Find the API URL in Terminal 1, then:
curl http://localhost:XXXX/flights
```

### Check Health
```powershell
curl http://localhost:XXXX/health
```

## ⚙️ Manual Start (Alternative)

If you prefer to start services individually:

### Terminal 1 - API
```powershell
cd FlightControl.Api
dotnet run
```
API will start on `http://localhost:5000`

### Terminal 2 - Simulator
```powershell
cd FlightControl.Sim
dotnet run
```
Simulator will connect to API on port 5000

### Terminal 3 - Dashboard
```powershell
cd FlightControl.Dashboard
dotnet run
```
Dashboard will start on `http://localhost:5001`

**Then open: http://localhost:5001**

## 🛑 Stopping Services

- Press `Ctrl+C` in each terminal window
- Or simply close the terminal windows

## 🐛 Troubleshooting

### Port Already in Use
If you see "Address already in use":
```powershell
# Kill the process using the port
netstat -ano | findstr :5000
taskkill /PID <PID> /F
```

### Services Can't Connect
Make sure all three services are running:
1. API must start first
2. Simulator needs API to be running
3. Dashboard needs API to be running

### Dashboard Shows "Connecting to Radar..."
- Check that the API is running
- Check the API URL in the Dashboard terminal
- Verify no firewall is blocking localhost connections

## 📊 Expected Behavior

✅ **API**: Should show "Now listening on..." and health check logs
✅ **Simulator**: Should show "Flight Simulator Started" and update logs
✅ **Dashboard**: Should show the URL and Blazor connection logs

## 🎯 Success Indicators

When everything is working:
- ✅ You see 5 flights in the left sidebar
- ✅ Plane icons appear on the radar map
- ✅ Planes move smoothly every second
- ✅ Statistics show "5 Total In-Sector Flights"
- ✅ No error messages in any terminal

## 🚀 Next Steps

1. **Explore the Dashboard** - Click different flights, watch the radar
2. **Check the API** - Try the `/flights` endpoint
3. **View the Code** - See how real-time updates work
4. **Customize** - Add more flights, change colors, modify update frequency

## 📚 Documentation

- **Full Documentation**: See `README.md`
- **Project Summary**: See `PROJECT_SUMMARY.md`
- **Architecture Details**: See `README.md` → Architecture section

---

**Need Help?** Check the terminal outputs for error messages and ensure all prerequisites are installed.

**Enjoy your Flight Control Dashboard! ✈️**
