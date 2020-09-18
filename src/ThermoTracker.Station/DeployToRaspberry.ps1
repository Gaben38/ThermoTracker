docker context use remote
docker build -t thermo-tracker-station-image -f Dockerfile .
docker create --name thermo-tracker-station thermo-tracker-station-image