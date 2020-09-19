docker context use remote
docker rm thermo-tracker-station
docker run --device /dev/gpiomem --name thermo-tracker-station -d thermo-tracker-station-image