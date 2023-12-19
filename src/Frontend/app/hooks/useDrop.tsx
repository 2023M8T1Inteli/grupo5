import { useState } from "react";

export interface IDroppedItem {
	icon: any;
	commandName: string;
	pairableItems: string[];
}

const useDrop = (initialItems: IDroppedItem[]) => {
    const [droppedItems, setDroppedItems] = useState(initialItems);

    const handleDragOver = (event: React.DragEvent) => {
        event.preventDefault();
    };

    const handleDrop = async (event: React.DragEvent) => {
        event.preventDefault();
        const data = JSON.parse(event.dataTransfer.getData("text/plain"));
        
        if (droppedItems.length > 0) {
            const lastDroppedItem = droppedItems[droppedItems.length - 1];
            if (!lastDroppedItem.pairableItems.includes(data.commandName)) {
                return;
            }
        }

        setDroppedItems(prevItems => [...prevItems, data]);
    };

    return [droppedItems, handleDragOver, handleDrop] as const;
}

export default useDrop;
