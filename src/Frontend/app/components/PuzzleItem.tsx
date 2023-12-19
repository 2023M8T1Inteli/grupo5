import Image from "next/image";
import { useEffect, useState } from "react";

export default function PuzzleItem({icon, commandName, pairableItems, lastDroppedItem, blockType} : {icon: any, commandName : string, pairableItems: string[], blockType: string, lastDroppedItem: {icon: any, commandName: string, pairableItems: string[], blockType: string} | null}) {
	const [canDrop, setCanDrop] = useState(true);

	useEffect(() => {
		if (lastDroppedItem) {
			setCanDrop(lastDroppedItem.pairableItems.includes(commandName));
		} else {
			setCanDrop(true);
		}
	}, [lastDroppedItem, commandName]);

	const handleDragStart = (event: React.DragEvent) => {
		event.dataTransfer.setData("text/plain", JSON.stringify({ icon, commandName, pairableItems }));
	};

	return (
		<div 
			className={`flex flex-col justify-center  items-center gap-3 ${canDrop ? 'cursor-grab' : 'cursor-not-allowed'}`}
			draggable={canDrop}
			onDragStart={handleDragStart}
			style={{ opacity: canDrop ? 1 : 0.2 }}
		>
			<Image onDrag={() => { return false}} draggable='false' src={icon} alt={commandName} />
			<span className='text-black text-base font-light'>{commandName}</span>
		</div>
	)
}
