import debugpy
import sys

# Get the argument information
pyExe = sys.argv[0]
host  = sys.argv[1]
port  = sys.argv[2]

# Configure the module
debugpy.configure(python=pyExe)

# Create a host debug server
print('Creating a debug server on {}:{}'.format(host, port), flush=True)
debugpy.listen((host, port))

# Wait for the client connection
print('Waiting for client debug connection', flush=True)
debugpy.wait_for_client()
print('Connected to client', flush=True)