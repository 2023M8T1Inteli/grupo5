import BoardQuadrant from "./BoardQuadrant";

export default function BoardIcons({ handleQuadrantClick, activeQuadrant, chunkedBoardIcons }: { handleQuadrantClick: (index: number) => void, activeQuadrant: number, chunkedBoardIcons: string[][] }) {
    return (
        <div className='flex flex-col bg-[#EEEEEE] w-[22%] h-[100%] items-center justify-center gap-4'>
            {chunkedBoardIcons.map((chunk, i) => (
                <div className='flex gap-4' key={i}>
                    {chunk.map((icon, j) => (
                        <div key={j} onClick={() => handleQuadrantClick(i * 3 + j)}>
                            <BoardQuadrant icon={icon} active={activeQuadrant === i * 3 + j} />
                        </div>
                    ))}
                </div>
            ))}
        </div>
    )
}