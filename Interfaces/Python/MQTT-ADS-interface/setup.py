from setuptools import setup

SETUP_REQUIRES = [
    'pyads >= 3.3.5',
    'paho-mqtt >= 1.5.0',
    'pytz'
]

setup(
    name='MQTT-ADS-interface',
    version='0.1.0',
    url='https://github.com/RWTH-EBC/AixOCAT/tree/i52_MQTT-pyADS-Interface/Interfaces/Python/MQTT-ADS-interface',
    license='',
    author='Markus Schraven, Stephan Göbel',
    author_email='',
    description='',
    setup_requires = SETUP_REQUIRES,
    install_requires = SETUP_REQUIRES
)
