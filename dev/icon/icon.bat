::magick convert icon.png -define icon:auto-resize=16,48,256 -compress zip icon.ico
magick convert icon.png -define icon:auto-resize=48 -compress zip icon.ico
pause