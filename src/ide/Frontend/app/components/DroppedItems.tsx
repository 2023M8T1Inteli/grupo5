import { IDroppedItem } from "../hooks/useDrop";
import DroppedItem from "./DroppedItem";

export default function DroppedItems({ droppedItems, handleDragOver, handleDrop } : { droppedItems: IDroppedItem[], handleDragOver: (e: React.DragEvent<HTMLDivElement>) => void, handleDrop: (e: React.DragEvent<HTMLDivElement>) => void }) {
    return (
        <div id='o' className='w-full h-[70%] flex justify-center items-center gap-8' onDragOver={handleDragOver} onDrop={handleDrop}>
            {droppedItems.map((item, index) => (
                <DroppedItem key={index} item={item} index={index} />
            ))}
        </div>
    )
}