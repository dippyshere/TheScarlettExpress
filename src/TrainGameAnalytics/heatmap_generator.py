import os
import json
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
from scipy.ndimage import gaussian_filter

class TrainGameAnalytics:
    def __init__(self, data_folder):
        self.data_folder = data_folder
        self.location_data = {}

    def load_json_files(self):
        for filename in os.listdir(self.data_folder):
            if filename.endswith(".json"):
                file_path = os.path.join(self.data_folder, filename)
                with open(file_path, 'r') as file:
                    data = json.load(file)
                    self.process_data(data)

    def process_data(self, data):
        for event in data:
            if event['eventName'] in ['character_movement', 'jump']:
                scene = event['eventData']['scene']
                x, z = event['eventData']['position']['x'], event['eventData']['position']['z']
                if scene not in self.location_data:
                    self.location_data[scene] = []
                self.location_data[scene].append((x, z))

    def generate_heatmaps(self):
        for scene, locations in self.location_data.items():
            x_coords, z_coords = zip(*locations)

            # resolution of the heatmap
            bins = 200

            # range of the heatmap centered around (0, 0)
            match scene:
                case "OpeningScene":
                    x_range = (-60, 35)
                    z_range = (-60, 35)
                case "Tutorial":
                    x_range = (-45, 70)
                    z_range = (-45, 70)
                case "RestrauntTutorial":
                    x_range = (-45, 70)
                    z_range = (-45, 70)
                case "StationTutorial":
                    x_range = (-15, 65)
                    z_range = (-15, 65)
                case "PlayerTesting":
                    x_range = (-45, 70)
                    z_range = (-45, 70)
                case "Station1":
                    x_range = (-15, 65)
                    z_range = (-15, 65)
                case "Station2":
                    x_range = (-15, 65)
                    z_range = (-15, 65)
                case "Station3":
                    x_range = (-15, 65)
                    z_range = (-15, 65)
                case _:
                    x_range = (-70, 70)
                    z_range = (-70, 70)

            heatmap, xedges, yedges = np.histogram2d(x_coords, z_coords, bins=bins, range=[x_range, z_range])

            # smoothing the heatmap
            heatmap = gaussian_filter(heatmap, sigma=1)

            heatmap = np.log1p(heatmap)

            sns.heatmap(heatmap.T, cmap="YlGnBu", cbar=True, xticklabels=False, yticklabels=False)
            plt.title(f"Heatmap for Scene: {scene}")
            plt.xlabel("X Position")
            plt.ylabel("Z Position")
            plt.gca().invert_yaxis()
            plt.savefig(f"{scene}_heatmap.png", transparent=True, bbox_inches='tight', pad_inches=0)
            plt.show()


if __name__ == "__main__":
    data_folder = os.path.join(os.path.dirname(__file__), 'Data')
    analytics = TrainGameAnalytics(data_folder)
    analytics.load_json_files()
    analytics.generate_heatmaps()
