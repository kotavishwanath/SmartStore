export const getRandomArbitrary = (min, max) => {
  return Math.round(Math.random() * (max - min) + min);
};

export const search = (nameKey, myArray) => {
  let i;
  for (i = 0; i < myArray.length; i++) {
    if (myArray[i].productId === nameKey) {
      return myArray[i];
    }
  }
};
