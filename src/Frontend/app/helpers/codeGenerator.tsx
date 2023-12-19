import { IDroppedItem } from "../hooks/useDrop";

export const commandNameMapping = (activeQuadrant : number) => ({
    'Se': 'se ',
    'Apertar': `quadrante == ${activeQuadrant} entao inicio\n`,
    'Mostrar imagem': `mostrar()`,
    'E': `\n`,
    'Tocar som': `tocar()`,
    'E fim': `\nfim\n`
});

export const generateCommandCode = (droppedItems : IDroppedItem[], commandMapping : { [key: string]: string }) => {
    return droppedItems.reduce((code, item) => {
        return code + (commandMapping[item.commandName] || '');
    }, '');
}

export const generateRawCode = (
    therapyName: string, 
    droppedItems: IDroppedItem[], 
    commandMapping: { [key: string]: string }, 
    imagePath: string | null, 
    audioPath: string | null
) => {
    let code = `programa "${therapyName}":\n`;
    if (imagePath) {
        code += `imagem = "${imagePath}"\n`;
    }
    if (audioPath) {
        code += `audio = "${audioPath}"\n`;
    }
    code += 'inicio\n';
    code += 'quadrante = ler()\n';
    code += generateCommandCode(droppedItems, commandMapping);
    code += 'fim.\n';
    if (droppedItems.some(e => e.commandName === 'E fim')) {
        console.log(code);
    }
    return code;
}
