import debugpy
import sys

# Get the argument information
pyExe = sys.argv[0]
host  = sys.argv[1]
port  = sys.argv[2]

# Set the python executable. Debugpy assumes sys.executable is the pyton path so it
# can create a debug server
inputExe       = sys.executable
sys.executable = pyExe

# Create a host debug server
print('Creating a debug server on {}:{}'.format(host, port), flush=True)
debugpy.listen((host, int(port)))

# Wait for the client connection
print('Waiting for client debug connection', flush=True)
debugpy.wait_for_client()
print('Connected to client', flush=True)

# Restore the executable path
sys.executable = inputExe